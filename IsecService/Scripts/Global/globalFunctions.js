

function Load(msg) {
    $('body').loadingModal({
        position: 'auto',
        text: msg,
        color: '#fff',
        opacity: '0.7',
        backgroundColor: 'rgb(0,0,0)',
        animation: 'doubleBounce'
    });
}
function Close() {
    $('body').loadingModal('hide');
    $('body').loadingModal('destroy');
}


$(document).ready(function () {

 
    if (window.sessionStorage.getItem("userLogged") !== "") {
        var user = JSON.parse(window.sessionStorage.getItem("userLogged"));
        $("#txtNombre").text(user.Nombre); 

    }  
}); 
$(document).on("click", "#btnLogout", function () {
    window.sessionStorage.setItem("userLogged", "");
    window.location.href = Router.action("Account", "Index");
});
$('#menuSidebar > a')
    .click(function (e) {
        //$('#menuSidebar > a').removeClass('active');
        let controller = $(this).data("controller");
        let action = $(this).data("action");
        window.location.href = Router.action(controller, action);
        $(this).toggleClass('active');
    });