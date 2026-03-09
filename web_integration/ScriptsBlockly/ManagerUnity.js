// Verifica se já existe o mapa de teclas pressionadas
if (!window.nearDistance) window.nearDistance = new Set();
if (!window.farDistance) window.farDistance = new Set();
 
//inventory Table
if (!window.inventory) window.inventory = new Map(); 

window.addEventListener("message", function(event) {
  console.log("Recebido do Unity:", event.data);
  if (!event.data || !event.data.type || !event.data.id) return;
//   if ((!event.data || !event.data.type || event.data.type === "Level") || 
//         (!event.data || !event.data.type || !event.data.id)) return;
  
  if (event.data.type === "near") {
        if (event.data.data === "add") {
            window.nearDistance.add(event.data.id);
            // console.log(`Adicionado a nearDistance: ${event.data.id}`);
        } else if (event.data.data === "remove") {
            window.nearDistance.delete(event.data.id);
            console.log(`Removido de nearDistance: ${event.data.id}`);
        }
    }

    if (event.data.type === "far") {
        if (event.data.data === "add") {
            window.farDistance.add(event.data.id);
            // console.log(`Adicionado a farDistance: ${event.data.id}`);
        } else if (event.data.data === "remove") {
            window.farDistance.delete(event.data.id);
            // console.log(`Removido de farDistance: ${event.data.id}`);
        }
    }

    
    //table Inventory
    if (event.data.type === "inventory") { 
        // let count = event.data.count || 1; // Se não houver count, assume 1 por padrão 
        // console.log(`Alterando inventory: ${event.data.id}, Count: ${count}`);
        // if (event.data.data === "add") { window.inventory.add(event.data.id);  
        //     } 
        // else if (event.data.data === "remove") { window.inventory.delete(event.data.id);  }
        //=================================================================================================

        let count = event.data.count || 1; // Se não houver count, assume 1 por padrão
        console.log(`Alterando inventory: ${event.data.id}, Count: ${count}`);

        if (event.data.data === "add") {
            if (window.inventory.has(event.data.id)) {
                window.inventory.set(event.data.id, window.inventory.get(event.data.id) + count);
            } else {
                window.inventory.set(event.data.id, count);
            }
            console.log("resultado inventory + " + window.inventory.get(event.data.id));
        } else if (event.data.data === "remove") {
            if (window.inventory.has(event.data.id)) {
                let newCount = window.inventory.get(event.data.id) - count;
                if (newCount > 0) {
                    window.inventory.set(event.data.id, newCount);
                } else {
                    window.inventory.delete(event.data.id);
                }
            }
        }


    }



    //Levels
    if (event.data.type === "Level") { 
        console.log("chegou llevel");
        adicionarPrimeiraColecao(CurrentCollection);
    }
    if (event.data.type === "updateToolbox") { 
        resetWorkspace(workspace)
        updateToolbox(event.data.data);                     //updateToolbox(event.data.data);
    }

});


function ShowAlert() {
  alert("Hello from Unity bb show!");
}



//focus
    
var iframe = document.getElementById("unityIframe");
function sendEventToIframe(event) {
    let iframeDocument = iframe.contentDocument || iframe.contentWindow.document;
        if (iframeDocument) {
            let keyboardEvent = new KeyboardEvent(event.type, event);
            iframeDocument.dispatchEvent(keyboardEvent);
        }
}
