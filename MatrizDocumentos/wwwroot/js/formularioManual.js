
/* *******************************************************
** Código JavaScript para editar los datos de una tabla **
** Raul Eduardo Bolaños Muñoz                           **
*********************************************************/

var miTabla = 'tabla';


function iniciarTabla() {
    tab = document.getElementById(miTabla);
    filas = tab.getElementsByTagName('tr');
    for (i = 1; fil = filas[i]; i++) {
        celdas = fil.getElementsByTagName('td');
        for (j = 1; cel = celdas[j]; j++) {
            if ((i > 0 && j == celdas.length - 1) || (i == filas.length - 1 && j != 0)) continue; 
            cel.onclick = function () { crearInput(this) }
        } 
    } 
    sumar();
    //
   

    
    // añadir funcion onclick a las imágenes para borrar y añadir y ocultar las imágenes de borrar
    for (i = 0; im = document.images[i]; i++)
        if (im.alt == 'añadir fila')
            im.onclick = function () { anadir(this, 0) }
        else if (im.alt == 'añadir columna')
            im.onclick = function () { anadir(this, 1) }
        else if (im.alt == 'borrar fila') {
            im.onclick = function () { borrar(this, 1) }
            im.style.visibility = 'hidden';
        }
        else if (im.alt == 'borrar columna') {
            im.onclick = function () { borrar(this, 1) }
            im.style.visibility = 'hidden';
        }


} 

// crear input para editar datos
function crearInput(celda) {

    celda.onclick = function () { return false }
    txt = celda.innerHTML;
    celda.innerHTML = '';
    obj = celda.appendChild(document.createElement('input'));
    obj.value = txt;
    obj.focus();
    obj.onblur = function () {
        txt = this.value;
        celda.removeChild(obj);
        celda.innerHTML = txt;
        celda.onclick = function () { crearInput(celda) }
        sumar();
    }
}

function sumar() {

    tab = document.getElementById(miTabla);

    filas = tab.getElementsByTagName('tr');
    sum = new Array(filas.length);
    for (i = 0; i < sum.length; i++)
        sum[i] = 0;
    for (i = 1, tot = filas.length - 1; i < tot; i++) {
        total = 0;
        celdas = filas[i].getElementsByTagName('td');
        for (j = 3, to = celdas.length - 1; j < to; j++) {

            //cantidad
            var cantidad = celdas[2].innerHTML;
            if (isNaN(cantidad)) {
                celdas[2].innerHTML = 0;
                cantidad = 0;
            }
            //valorunitario
            var valorunitario = celdas[3].innerHTML;
            if (isNaN(valorunitario)) {
                celdas[3].innerHTML = 0;
                cantidad = 0;
            }
            //cantidad
            cantidad = parseFloat(celdas[2].innerHTML);
            if (isNaN(cantidad)) cantidad = 0;

            num = parseFloat(celdas[j].innerHTML);
            if (isNaN(num)) num = 0;
            total += num * cantidad;
            sum[j - 3] += num;
        } 

        if (isNaN(cantidad)) cantidad = 0;
        celdas[celdas.length - 1].innerHTML = total;
        sum[j - 3] += total;
    } 

    if (isNaN(cantidad)) cantidad = 0;
    subt = filas[filas.length - 1].getElementsByTagName('td');
    for (i = 3, tot = subt.length; i < tot; i++)

        subt[i].innerHTML = sum[i - 3];
} 

// añadir filas o columnas
function anadir(obj, num) {
    if (num == 0) { // añadir filas
        fila = obj.parentNode.parentNode;
        nuevaFila = fila.cloneNode(true);
        
        im = nuevaFila.getElementsByTagName('img');
        im[0].onclick = function () { anadir(this, 0) }
        im[1].onclick = function () { borrar(this, 0) }
        for (i = 1, tot = nuevaFila.getElementsByTagName('td').length - 1; i < tot; i++) {
            nuevaFila.getElementsByTagName('td')[i].innerHTML = '';
            nuevaFila.getElementsByTagName('td')[i].onclick = function () { crearInput(this) };
            nuevaFila.getElementsByTagName('td')[i].innerHTML;
        }
        insertAfter(nuevaFila, fila);
        sumar();
    } 

    else { // añadir columnas
        tab = document.getElementById(miTabla);
        cabecera = tab.getElementsByTagName('tr')[0];
        for (num = 0; cel = cabecera.getElementsByTagName('td')[num]; num++)
            if (cel == obj.parentNode) break;
        for (i = 0; fila = tab.getElementsByTagName('tr')[i]; i++) {
            ele = fila.getElementsByTagName('td')[num];
            nuevaColumna = ele.cloneNode(true);
            if (i == 0) {
                im = nuevaColumna.getElementsByTagName('img');
                im[0].onclick = function () { anadir(this, 1) }
                im[1].onclick = function () { borrar(this, 1) }
            }
            else {
                nuevaColumna.innerHTML = (i == 1) ? '' : 0;
                nuevaColumna.onclick = function () { crearInput(this) };
            }
            fila.insertBefore(nuevaColumna, ele);
        } 
    } 
    mostrarImagenes();
}

function insertAfter(newElement, targetElement) {
    var parent = targetElement.parentNode;
    if (parent.lastchild == targetElement) {
        parent.appendChild(newElement);
    } else {
        parent.insertBefore(newElement, targetElement.nextSibling);
    }
}

// borrar filas o columnas 
function borrar(obj, num) {

    contador_temporal = 0;
    $('#tabla tr').each(function (row, tr) {
        contador_temporal++;
    });

    if (num == 0 || contador_temporal > 3) { 
        tab = obj.parentNode.parentNode.parentNode;
        tab.removeChild(obj.parentNode.parentNode);
    } 
    else { 
      
        tab = document.getElementById(miTabla);
        cabecera = tab.getElementsByTagName('tr')[0];
        for (num = 0; cel = cabecera.getElementsByTagName('td')[num]; num++)
            if (cel == obj.parentNode) break;
        for (i = 0; fila = tab.getElementsByTagName('tr')[i]; i++)
            fila.removeChild(fila.getElementsByTagName('td')[num]);
    }
    sumar();
    mostrarImagenes();
}

// mostrar/ocultar imagenes para borrar
function mostrarImagenes() {
    tab = document.getElementById(miTabla);
    filas = tab.getElementsByTagName('tr');
    columnas = filas[0].getElementsByTagName('td');
    numFilas = filas.length;
    numColumnas = columnas.length;
    for (i = 0; im = tab.getElementsByTagName('img')[i]; i++)
        if (im.alt == 'borrar fila')
            im.style.visibility = (numFilas > 1) ? 'visible' : 'hidden';
        else if (im.alt == 'borrar columna')
            im.style.visibility = (numColumnas > 1) ? 'visible' : 'hidden';
}


// Obtiene la Lista de Proveedores
function selectNit(proveedores) {

    var value = document.getElementById("ValNit");
    var id = value.options[value.selectedIndex].value;

    var NIT;
    var CONTACTO;
    var EMAIL;
    var TELEFONO;

    $.ajax({
        type: 'GET',
        async: false,
        url: 'Ordenes/ObtenerProveedores',
        data: { id },
        type: "post",
        success: function (result) {

            $.each(proveedores.ProveedorList, function (i, result) {
                if (id == result.ID_PROVEEDOR) {
                    NIT = result.NIT;
                    CONTACTO = result.CONTACTO;
                    EMAIL = result.EMAIL;
                    TELEFONO = result.TELEFONO;
                }
            });
            document.getElementById("id_proveedor").value = id;
            document.getElementById("nit").value = NIT;
            document.getElementById("contacto").value = CONTACTO;
            document.getElementById("email").value = EMAIL;
            document.getElementById("telefono").value = TELEFONO;

        }
    });
}

// Enviar datos
function enviar(datos) {

    var data = new Object();
    var resul = false;

    var valorM = document.getElementById("Moneda");
    var valorM = valorM.options[valorM.selectedIndex].value;

    let id = (id) => document.getElementById(id);
    let classes = (classes) => document.getElementsByClassName(classes);

    let fechasolicitud = id("fechasolicitud"),
        Solicitante = id("Solicitante"),
        codigodelproyecto = id("codigodelproyecto"),
        codigopresupuestos = id("codigopresupuestos"),

        /*   codigopresupuestos = id("codigopresupuestos"),*/
        nit = id("nit"),
        contacto = id("contacto"),
        email = id("email"),
        telefono = id("telefono"),
        tabla = id("tabla"),
        garantia = id("garantia"),
        acuerdosdepago = id("acuerdosdepago"),
        incumplimiento = id("incumplimiento"),
        entregas = id("entregas"),
        seguimiento = id("seguimiento"),
        soporteposventa = id("soporteposventa"),
        otros = id("otros"),
        form = id("form"),
        errorMsg = classes("error")
       
    form.addEventListener("submit", (e) => {
        e.preventDefault();

        engine(fechasolicitud, 0, "Fecha de Solicitud no puede estar vacía");
        engine(Solicitante, 1, "Solicitante no puede estar vacío");
        engine(codigodelproyecto, 2, "Codigo del proyecto no puede estar vacío");
        engine(codigopresupuestos, 3, "Codigo de presupuesto no puede estar vacío");
        engine(nit, 4, "Nit no puede estar vacío");
        engine(contacto, 5, "Contacto no puede estar vacío");
        engine(email, 6, "Email no puede estar vacío");
        engine(telefono, 7, "Telefono no puede estar vacío");
        engine(garantia, 8, "Garantia no puede estar vacío");
        engine(acuerdosdepago, 9, "Acuerdos de pago no puede estar vacío");
        engine(incumplimiento, 10, "Incumplimiento no puede estar vacío");
        engine(entregas, 11, "Entregas no puede estar vacío");
        engine(seguimiento, 12, "Seguimiento no puede estar vacío");
        engine(soporteposventa, 13, "Soporte posventa no puede estar vacío");
        engine(otros, 14, "Otros no puede estar vacío");
        DesabilitarBoton();
    });

    let engine = (id, serial, message) => {
        if (id.value.trim() === "") {
            errorMsg[serial].innerHTML = message;
            id.style.border = "2px solid red";
        } else {
            errorMsg[serial].innerHTML = "";
            id.style.border = "2px solid green";
        }
    };

    //DATOS GENERALES
    data.fechasolicitud = $("#fechasolicitud").val();
    data.Solicitante = $("#Solicitante").val();
    data.codigodelproyecto = $("#codigodelproyecto").val();
    data.id_ordencompra = $("#idOrdendecompra").val();
    data.Moneda = valorM;
    if (isNaN(data.codigodelproyecto)) {

        codigodelproyecto.value = '';
    }

    data.codigopresupuestos = $("#codigopresupuestos").val();

    //DATOS DEL PROVEEDOR
    data.id_proveedor = $("#id_proveedor").val();
    data.nit = $("#nit").val();
    data.contacto = $("#contacto").val();
    data.email = $("#email").val();
    data.telefono = $("#telefono").val();

    //CARACTERISTICAS COMERCIALES
    data.garantia = $("#garantia").val();
    data.acuerdosdepago = $("#acuerdosdepago").val();
    data.incumplimiento = $("#incumplimiento").val();
    data.entregas = $("#entregas").val();
    data.seguimiento = $("#seguimiento").val();
    data.soporteposventa = $("#soporteposventa").val();
    data.otros = $("#otros").val();

    var TableData = new Array();

    $('#tabla tr').each(function (row, tr) {
        TableData[row] = {
            "Descripcion": $(tr).find('td:eq(1)').text()
            , "Cantidad": $(tr).find('td:eq(2)').text()
            , "ValorUnitario": $(tr).find('td:eq(3)').text()
            , "TotalAntesIva": $(tr).find('td:eq(4)').text()
            , "Total": $(tr).find('td:eq(4)').text()
        }
    });
    console.log(TableData);
    data.tabla = TableData;

    if (data.fechasolicitud == "" || data.Solicitante == "" || data.codigodelproyecto == "" || data.codigopresupuestos == "" || data.nit == "" || data.contacto == "" || data.email == "" || data.telefono == "" || data.tabla[1].Descripcion == "" || data.tabla[1].cantidad == "" || data.tabla[1].ValorUnitario == "" || data.tabla[1].total == "" || data.garantia == "" || data.acuerdosdepago == "" || data.incumplimiento == "" || data.entregas == "" || data.seguimiento == "" || data.soporteposventa == "" || data.otros == "") {
        resul = true;
    }
    else {
        
        $.ajax({
            url: 'Ordenes/ObtenerOrden',
            data: { data: data },
            type: 'GET',
            type: 'POST',
            success: function (e) {
                
                $("#ExportToExcel").click();
                setTimeout(() => { window.location.reload(); }, 2000);
            }
        });
        
    }
   
}

//Desabilitar boton desapues de enviar la solicitud

function DesabilitarBoton() {

    
    let botonEnviar = document.querySelector("#btnExportToExcel");
    let botonExport = document.querySelector("#ExportToExcel");

    botonEnviar.disabled = true;
    botonExport.disabled = true;
    botonEnviar.classList.add('boton-orden-compra');

     setTimeout(function () {
         botonEnviar.disabled = false;
         botonExport.disabled = false;
    }, 3000);
}

function TemporalCrearCampo() {
    tab = document.getElementById(miTabla);
    filas = tab.getElementsByTagName('tr');
    for (i = 1; fil = filas[i]; i++) {
        celdas = fil.getElementsByTagName('td');
        for (j = 1; cel = celdas[j]; j++) {
            if ((i > 0 && j == celdas.length - 1) || (i == filas.length - 1 && j != 0)) continue;
            cel.onclick = function () { crearInput(this) }
        }
    }
    for (i = 0; im = document.images[i]; i++)
        if (im.alt == 'añadir fila')
            im.onclick = function () { anadir(this, 0) }
        else if (im.alt == 'añadir columna')
            im.onclick = function () { anadir(this, 1) }
        else if (im.alt == 'borrar fila') {
            im.onclick = function () { borrar(this, 1) }
            im.style.visibility = 'hidden';
        }
        else if (im.alt == 'borrar columna') {
            im.onclick = function () { borrar(this, 1) }
            im.style.visibility = 'hidden';
        }
}





