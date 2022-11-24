//load
$(document).ready(function () {
    document.getElementById('txtUsername').focus();
});
function Close() {
    $('body').loadingModal('hide');
    $('body').loadingModal('destroy');
}

//click login
$(document).on("click", "#btnLogin", function () {
    let us = $("#txtUsername").val();
    let psw = $("#txtPassword").val();
    if (us !== "" && psw !== "") { 

        $('body').loadingModal({
            position: 'auto',
            text: "Ingresando...",
            color: '#fff',
            opacity: '0.7',
            backgroundColor: 'rgb(0,0,0)',
            animation: 'doubleBounce'
        });
        setTimeout(function () {
            let url = Router.action("Account", "Login", { username: us, password: psw });
            $.post(url, function (user) {
                let obj = JSON.parse(user);
                if (obj != null) {
                    window.sessionStorage.setItem("userLogged", user);
                    Close();
                    window.location.href = Router.action("Dashboard", "Index");
                }
                else {
                    Close();
                    Swal.fire({
                        title: 'Información incorrecta',
                        text: 'Por favor verifique bien sus datos, el usuario no existe',
                        icon: 'info'
                    });
                    document.getElementById('txtUsername').focus();
                }
            });
        }, 2000);  
       
    }
    else {
        Swal.fire({
            title: 'Datos incompletos',
            text: 'Por favor escriba usuario y contraseña',
            icon: 'info'
        });
    } 
});