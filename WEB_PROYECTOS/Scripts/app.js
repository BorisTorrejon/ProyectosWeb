function ValidarFechas(dateIni, dateFin) {
    var _dateIni = new Date(dateIni);
    var _dateFin = new Date(dateFin);
    if (_dateFin < _dateIni)
        return false;
    else
        return true;
}
function LoadingOverlayShow(id) {
    $(id).LoadingOverlay("show", {
        color: "rgba(255, 255, 255, 0.5)",
        image: "/Content/loading.gif",
        imageResizeFacto: 0.6,
        //imageAnimation: "1.5s fadein,"
    }
    );
}
function LoadingOverlayHide(id) {
    $(id).LoadingOverlay("hide")
}

function getDepartamento(id) {
    $.ajax({
        type: "GET",
        url: '/Departamento/GetDepartamentos',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                if (item.DepartamentoID == id) {
                    $("#DepartamentoID").append('<option value="' + item.DepartamentoID + ' " selected = "">' + item.NombreDepartamento + '</option>');
                } else {
                    $("#DepartamentoID").append('<option value=' + item.DepartamentoID + '>' + item.NombreDepartamento + '</option>');
                }
            });
        },
    });
}

function getProyecto() {
    $.ajax({
        type: "GET",
        url: '/Proyecto/FListarProyectos',
        dataType: "json",
        success: function (result) {
            console.log(result);
            $.each(result.proyectos, function (key, item) {
                $("#ProyectoID").append('<option value=' + item.ProyectoID + '>' + item.NombreProyecto+'</option>');
            });
        },
    });
}

function getEmpleado() {
    $.ajax({
        type: "GET",
        url: '/Empleado/ListarEmpleados',
        dataType: "json",
        success: function (result) {
            console.log(result);
            $.each(result.empleado, function (key, item) {
                console.log(item);
                $("#EmpleadoID").append('<option value=' + item.EmpleadoID + '>' + item.NombreCompleto + '</option>');
            });
        },
    });
}
function getAsignarProyecto() {
    $.ajax({
        type: "GET",
        url: '/Proyecto/getAsignarProyecto',
        dataType: "json",
        success: function (result) {
            $.each(result.data, function (key, item) {
                if (item.DepartamentoID == id) {
                    $("#DepartamentoID").append('<option value="' + item.DepartamentoID + ' " selected = "">' + item.NombreDepartamento + '</option>');
                } else {
                    $("#DepartamentoID").append('<option value=' + item.DepartamentoID + '>' + item.NombreDepartamento + '</option>');
                }
            });
        },
    });
}