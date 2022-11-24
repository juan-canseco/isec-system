
function LoadUsers() {
    $.getJSON(Router.action("Usuarios", "All"), function (data) {
        var table = $("#tblUsuarios").DataTable({
            dom: 'btpi',
            destroy: true,
            responsive: true,
            data: data,
            columns: [
                { data: 'Id' },
                { data: 'Nombre' },
                { data: 'Username' },
                { data: 'Password' }, 
                { data: 'Puesto' },
                {
                    title: 'Sincronizado',
                    data: null,
                    render: function (a, g, r) {
                        console.log(r.Sync);
                        let check = '';
                        if (r.Sync > 0) {
                            check = 'checked';
                        }
                        return `<div class="form-check form-switch"><input class="form-check-input" 
                                ${check} type="checkbox" role="switch" id="flexSwitchCheckDefault" readonly /></div>`;
                    }
                },
                {
                    title: 'Activo',
                    data: null,
                    render: function (a, g, r) {
                        let check = '';
                        if (r.Activo > 0) {
                            check = 'checked';
                        }
                        return `<label class="switch">
                                <input type="checkbox" ${check} readonly>
                                <span class="slider round"></span>
                                </label>`;
                    }
                },
                { data: 'LastUpdate' },
                {
                    data: 'Rol',
                    render: function (s, d, row) {
                        let r = "";
                        if (row.Rol !== null) {
                            r = row.Rol;
                        }
                        else {
                            r = "Sin rol";
                        }
                        return `${r}`;
                    }
                },
                {
                    title: '',
                    data: null,
                    render: function (a, g, r) {
                        return `<button type="button"   class="btn btn-sm btn-outline-secondary" id="btnEdit"><i class="fa fa-clipboard-check fa-2x"> </i></button>`;
                    }
                }
            ]
        });
        $("#txtFilter").keyup(function () {
            table.search($(this).val()).draw();
        }); 
        Close();
    });
}


$(document).ready(function () {
    $("select").select2({
        width: '100%',
        dropdownParent: $("#modalUsuarios")
    });
    Load("");
    document.getElementById("txtFilter").focus();
    LoadUsers(); 
    $(document).on("click", ".btnSave", function () {
        var name = $("#txtName").val();
        let puesto = $("#sPuesto option:selected").val();
        let us = $("#txtUsername").val();
        let psw = $("#txtPassword").val();
        if (name == "") {
            alert("es necesario nombre");
            document.getElementById("#txtName").focus();
            return;
        }
        else if (us == "") {
            alert("es necesario username");
            return;
        }
        else if (psw == "") {
            alert("es necesario una contraseña"); 
            return;
        }
        else if (puesto == 0) {
            alert("es necesario un puesto"); 
            return;
        }
        Load("Agregando usuario...");
        var user = {
            Id: 0,
            Nombre: name,
            Username: us,
            Password: psw,
            puestoid: puesto,
            Puesto: "",
            Sync: 0,
            Activo: 1,
            LastUpdate: null
        };
        var urlAdd = Router.action("Usuarios", "Add", {user: JSON.stringify(user)});
        $.post(urlAdd, function (id) {
            if (id > 0) { 
                Swal.fire({
                    title: 'Usuario registrado!',
                    text: 'Se ha registrado usuario correctamente.',
                    icon: 'success'
                });
                $("#modalUsuarios").modal("hide");
                $("#txtName").val(""); 
                $("#txtUsername").val("");
                $("#txtPassword").val("");
                LoadUsers();
            }
            else {
                Swal.fire({
                    title: 'Error!',
                    text: 'Hubo un problema al intentar registrar, verifique su información',
                    icon: 'error'
                });
                Close();
            }
        });
    }); 
    $(document).on("click", "#btnAdd", function () { 
        document.getElementById("txtNombre").blur();
        $("#modalUsuarios").modal("show"); 
    });
});