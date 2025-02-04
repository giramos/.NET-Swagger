document.addEventListener("DOMContentLoaded", init); // Cuando se cargue la página, se ejecuta la función search
const URL_API = 'https://localhost:7163/api/customer' // URL de la API
var customers = [] // Array de objetos con los datos de los clientes

function init() { search() } // Función que se ejecuta al cargar la página

//alert(1);//Muestra un mensaje de alerta 
function abrirFormulario() {
    //alert("Agregar Cliente");//Muestra un mensaje de alerta
    htmlModal = document.getElementById("modal"); //Obtiene el elemento con el id modal
    htmlModal.setAttribute("class", "modale opened"); //Agrega la clase open al elemento modal
}

function cerrarCliente() {
    //alert("Agregar Cliente");//Muestra un mensaje de alerta
    htmlModal = document.getElementById("modal"); //Obtiene el elemento con el id modal
    htmlModal.setAttribute("class", "modale"); //Agrega la clase open al elemento modal
}

function clean() { // Función para limpiar el formulario
    document.getElementById('txtId').value = ''
    document.getElementById('txtFirstName').value = ''
    document.getElementById('txtLastName').value = ''
    document.getElementById('txtPhone').value = ''
    document.getElementById('txtEmail').value = ''
    document.getElementById('txtAddress').value = ''
}

function agregar() {// Función para agregar un cliente
    clean() // Se limpia el formulario
    abrirFormulario() // Se abre el formulario
}


// Función que se ejecuta al cargar la página
async function search() {
    var url = URL_API // URL de la API
    var response = await fetch(url, {
        "method": 'GET',
        "headers": {
            "Content-Type": 'application/json'
        }
    })
    customers = await response.json();

    var html = ''
    for (customer of customers) {
        var row = `<tr> 
        <td>${customer.firstName}</td>
        <td>${customer.lastName}</td>
        <td>${customer.email}</td>
        <td>${customer.phone}</td>
        <td>
            <a href="#" onclick="edit(${customer.id})" class="myButton">Editar</a>
            <a href="#" onclick="remove(${customer.id})" class="myButtonDelete">Eliminar</a>
        </td>
    </tr>`
        html = html + row;
    }
    // Array de objetos con los datos de los clientes

    // Se añade la tabla al HTML
    document.querySelector("#customers > tbody").innerHTML = html

}
// Función para eliminar un cliente
async function remove(id) {
    // Se muestra un mensaje de confirmación
    var respuesta = confirm("¿Estás seguro de que quieres eliminar este cliente?")
    // Si la respuesta es afirmativa
    if (respuesta) {
        var url = URL_API + '/' + id // URL de la API
        await fetch(url, {
            method: 'DELETE',
            headers: {
                "Content-Type": 'application/json'
            }
        })

        // 1 version. Se actualiza
        window.location.reload() // Se recarga la página

        // 2 version. Se actualiza

        // .then(response => response.json()) // Se convierte la respuesta a JSON
        // .then(data => { // Se ejecuta la función search
        //     search()
        // })

        alert("Cliente eliminado") // Se muestra un mensaje de confirmación
    }

}

async function save() {
    // Se obtienen los datos del formulario
    var data = {
        "firstName": document.getElementById('txtFirstName').value,
        "lastName": document.getElementById('txtLastName').value,
        "phone": document.getElementById('txtPhone').value,
        "email": document.getElementById('txtEmail').value,
        "address": document.getElementById('txtAddress').value
    }

    var id = document.getElementById('txtId').value // Se obtiene el id del cliente
    // Si el id no está vacío, se añade al objeto
    if (id != '') {
        data.id = id
    }

    var url = URL_API // URL de la API
    await fetch(url, {
        "method": id != '' ? 'PUT' : 'POST', // Si el id no está vacío, se actualiza, si no, se crea
        "body": JSON.stringify(data), // Se convierte el objeto a un string
        "headers": {
            "Content-Type": 'application/json' // Se especifica que el contenido es JSON
        }
    })

    // 1 version. Se actualiza
    window.location.reload() // Se recarga la página

}

//
function edit(id) { // Función para editar un cliente
    // Se busca el cliente con el id seleccionado
    var customer = customers.find(x => x.id == id)
    // Se muestran los datos del cliente en el formulario
    document.getElementById('txtId').value = customer.id
    document.getElementById('txtFirstName').value = customer.firstName
    document.getElementById('txtLastName').value = customer.lastName
    document.getElementById('txtPhone').value = customer.phone
    document.getElementById('txtEmail').value = customer.email
    document.getElementById('txtAddress').value = customer.address
    // Se abre el formulario
    abrirFormulario()

}
