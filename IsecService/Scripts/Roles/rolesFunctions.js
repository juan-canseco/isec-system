function LoadRoles() {
    $.getJSON(Router.action("Roles", "Roles"), function (data) {
        var table = $("#tblRoles").DataTable({
            dom: 'btpi',
            destroy: true,
            responsive: true,
            processing: true, 
            data: data,
            columns: [
                { data: 'Id' },
                { data: 'Descripcion' },
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
                    title: '',
                    data: null,
                    render: function (a, g, r) {
                        return `<button type="button" data-id="${r.Id}" data-rol="${r.Descripcion}"  id="btnEdit" class="btn btn-sm btn-secondary"><i class="fa fa-clipboard-check fa-2x"> </i></button>`;
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
function LoadUsers() {

    $.getJSON(Router.action("Usuarios", "All"), function (data) {
        var s = $("#sUsuarios");
        s.html("");
        s.append(`<option value="0">-Seleccione-</option>`);
        $.each(data, function (i, v) {
            s.append(`<option value="${v.Id}">${v.Nombre}</option>`);
        });
    });
} 
function LoadAccesosByRol(rolid) {
    var url = Router.action("AccesosUsuario", "AllByRol", { id: rolid });
    $.post(url, function (data) {
        let accesos = JSON.parse(data);
        if (accesos.length > 0) {
            var table = $("#tblModulos").DataTable({
                dom: 'btpi',
                destroy: true,
                responsive: true,
                pageLength: 7,
                processing: true,
                data: accesos,
                columns: [
                    {
                        title: 'Acceso',
                        data: null,
                        render: function (s, a, r) {
                            let check = '';
                            if (r.Acceso == true) {
                                check = 'checked';
                            }
                            return `<label class="switch">
                                <input class="chAcceso" data-rol="${r.RolId}" data-moduloid="${r.ModuloId}" data-id="${r.Id}" data-acc="${r.Acceso}" data-l="${r.Lectura}" data-e="${r.Escritura}" type="checkbox" ${check} readonly>
                                <span class="slider round"></span>
                                </label>`;
                        }
                    },
                    { data: 'Modulo' },
                    {
                        title: 'Lectura',
                        data: null,
                        render: function (a, s, r) {
                            let check = '';
                            if (r.Lectura == true) {
                                check = 'checked';
                            }
                            return ` <label class="switch">
                                <input id="chLectura" data-rol="${r.RolId}" data-moduloid="${r.ModuloId}" data-id="${r.Id}" data-acc="${r.Acceso}" data-l="${r.Lectura}" data-e="${r.Escritura}" type="checkbox" ${check} readonly>
                                <span class="slider round"></span>
                                </label>`;
                        }
                    },
                    {
                        title: 'Escritura',
                        data: null,
                        render: function (a, f, r) {
                            let check = '';
                            if (r.Escritura == true) {
                                check = 'checked';
                            }
                            return ` <label class="switch">
                                <input id="chEscritura" data-rol="${r.RolId}" data-moduloid="${r.ModuloId}" data-id="${r.Id}" data-acc="${r.Acceso}" data-l="${r.Lectura}" data-e="${r.Escritura}" type="checkbox" ${check} readonly>
                                <span class="slider round"></span>
                                </label>`;
                        }
                    }
                ]
            });
            Close();
            LoadUsers(); 
            let rol = $("#btnEdit").data("rol");
            $("#btnAsignar").removeData("id");
            $("#btnAsignar").attr("data-id", rolid);
            $("#txtRol").val(rol);
            $("#modalModulos").modal("show"); 

        }
        else {
            Close(); 
            Swal.fire({
                title: "Sin permisos!",
                text: "No existen permisos para este rol",
                icon: "info"
            });
        }
      
       
    });
}
$(document).ready(function () {
    $("select").select2({
        width: '100%',
        dropdownParent: $("#modalModulos")
    });
    Load("");
    document.getElementById("txtFilter").focus();
    LoadRoles();

    //editar accesos a modulos
    $(document).on("click", "#btnEdit", function () {
        Load("Cargando permisos...");
        let id = $(this).data("id");
        LoadAccesosByRol(id); 
        
    });
    $(document).on("click", "#btnAdd", function () {
        $("#modalAdd").modal("show");
    });
    $(document).on("click", ".btnSaveRol", function () {
        let nombre = $("#txtNombreRol").val();
        if (nombre !== "") {
            Load("Registrando rol...");
            let rol = {
                Id: 0,
                Descripcion: nombre,
                Sync: 0,
                Activo: 1,
                LastUpdate:null
            };
            var url = Router.action("Roles", "Add", { rol: JSON.stringify(rol) });
            $.post(url, function (result) {
                if (result > 0) {
                    if (result == 1) {
                        Close();
                        Swal.fire({
                            title: "Rol registrado!",
                            text: "Se ha registrado con exito el rol.",
                            icon: "success"
                        });
                        LoadRoles();
                        $("#txtNombreRol").val("");
                        $("#modalAdd").modal("hide");

                    } else if (result == 2) {
                        Close();
                        Swal.fire({
                            title: "Rol existente!",
                            text: `El rol ${nombre} ya existe`,
                            icon: "info"
                        });
                    }
                }
                else {
                    Close();
                    Swal.fire({
                        title: "Rol no registrado!",
                        text: `Hubo un problema al intentar registrar rol.`,
                        icon: "error"
                    });
                }
            });
        }
        else {
            Swal.fire({
                title: "No ha escrito un rol",
                text: `Es necesario un nombre para registrar un nuevo rol.`,
                icon: "info"
            });
        }
    });
    $(document).on("change", ".chAcceso", function () {
        let acceso = false;
        if ($(this).is(":checked")) { 
            acceso = true;
        } 
        let permiso = {
            Id: $(this).data("id"),
            Acceso: acceso,
            Lectura: $(this).data("l"),
            Escritura: $(this).data("e"),
            RolId: $(this).data("rol"),
            Rol:"",
            ModuloId: $(this).data("moduloid"),
            Modulo: "",
            Sync: 0
        };
        var url = Router.action("AccesosUsuario", "Update", { permiso: JSON.stringify(permiso) });
        $.post(url, function (d) {

        });
    })
    $(document).on("change", "#chLectura", function () {
        let acceso = false;
        if ($(this).is(":checked")) { 
            acceso = true;
        } 
        let permiso = {
            Id: $(this).data("id"),
            Acceso:$(this).data("acc"),
            Lectura:acceso,
            Escritura: $(this).data("e"),
            RolId: $(this).data("rol"),
            Rol:"",
            ModuloId: $(this).data("moduloid"),
            Modulo: "",
            Sync: 0
        };
        var url = Router.action("AccesosUsuario", "Update", { permiso: JSON.stringify(permiso) });
        $.post(url, function (d) {

        });
    })
    $(document).on("change", "#chEscritura", function () {
        let acceso = false;
        if ($(this).is(":checked")) { 
            acceso = true;
        } 
        let permiso = {
            Id: $(this).data("id"),
            Acceso:$(this).data("acc"),
            Lectura:$(this).data("l"),
            Escritura: acceso,
            RolId: $(this).data("rol"),
            Rol:"",
            ModuloId: $(this).data("moduloid"),
            Modulo: "",
            Sync: 0
        }; 
        var url = Router.action("AccesosUsuario", "Update", { permiso: JSON.stringify(permiso) });
        $.post(url, function (d) {

        });
    })
    $(document).on("click", "#btnAsignar", function () {
        let id = $(this).data("id");
        let selected = $("#sUsuarios option:selected").val();
        if (selected > 0) {
            var obj = {
                RolId: id,
                Rol: "",
                UsuarioId: selected,
                Usuario: ""
            };
            var url = Router.action("RolUsuarios", "Add", { roluser: JSON.stringify(obj) });
            $.post(url, function (msg) {
                Swal.fire({
                    title: "AVISO!",
                    text: msg,
                    icon: "info"
                });
            });
        }
    });
});