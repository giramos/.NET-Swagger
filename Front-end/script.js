document.addEventListener("DOMContentLoaded", search); // Cuando se cargue la página, se ejecuta la función search


// Función que se ejecuta al cargar la página
function search() { 
    // Array de objetos con los datos de los clientes
    var resultado = [
        {
            "id": 12,
            "firstName": "string",
            "lastName": "string",
            "email": "fff@gmail.com",
            "address": "string",
            "phone": "string"
        },
        {
            "id": 13,
            "firstName": "nombre",
            "lastName": "apellido",
            "email": "email@email.com",
            "address": "direccion",
            "phone": "telefono"
        }
    ]
 // Array de objetos con los datos de los clientes
    var row = `<tr> 
            <td>José</td>
            <td>Mourinho</td>
            <td>jose@terra.es</td>
            <td>666111222</td>
            <td>
                <a href="#" class="myButton">Editar</a>
                <a href="#" class="myButtonDelete">Eliminar</a>
            </td>
        </tr>
        <tr>
            <td>Germán</td>
            <td>Iglesias</td>
            <td>german@terra.es</td>
            <td>666999000</td>
            <td>
                <a href="#" class="myButton">Editar</a>
                <a href="#" class="myButtonDelete">Eliminar</a>
            </td>
        </tr>`
    // Se añade la tabla al HTML
    document.querySelector("#customers > tbody").outerHTML = row; 

}
