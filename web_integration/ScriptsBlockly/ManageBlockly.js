function saveBlocks() {
    const workspaceState = Blockly.serialization.workspaces.save(Blockly.getMainWorkspace());
    localStorage.setItem("blockly_workspace", JSON.stringify(workspaceState));
    alert("Blocos salvos!");
}

function loadBlocks() {
    const savedState = localStorage.getItem("blockly_workspace");
    if (savedState) {
        Blockly.serialization.workspaces.load(JSON.parse(savedState), Blockly.getMainWorkspace());
        alert("Blocos carregados!");
    } else {
        alert("Nenhum bloco salvo.");
    }
}




//load scenes
function loadScene(sceneName) {
    // scene = sceneName;
    iframe.contentWindow.postMessage({
        type: "sendMessage",
        target: "Scene Manager",  // Nome do GameObject no Unity
        method: "MudarCena",   // Nome do método no Unity
        message: sceneName            // Mensagem que você deseja passar
        }, "*");
}

// let scene;


//Grid
function Grid() {
    // scene = sceneName;
    iframe.contentWindow.postMessage({
        type: "sendMessage",
        target: "WorldGridPaint",  // Nome do GameObject no Unity
        method: "Active",   // Nome do método no Unity
        message: ""            // Mensagem que você deseja passar
        }, "*");
}






//export and import
function saveBlocksToFile() {
    const workspaceState = Blockly.serialization.workspaces.save(Blockly.getMainWorkspace());
    const json = JSON.stringify(workspaceState);
    const blob = new Blob([json], { type: "application/json" });
    const url = URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = "blocos_blockly.json";
    a.click();

    URL.revokeObjectURL(url);
    alert("Blocos salvos em arquivo!");
}

function loadBlocksFromFile() {
    const input = document.getElementById("fileInput");
    input.click();

    input.onchange = () => {
        const file = input.files[0];
        if (!file) {
            alert("Nenhum arquivo selecionado.");
            return;
        }

        const reader = new FileReader();
        reader.onload = function (e) {
            try {
                const json = JSON.parse(e.target.result);
                Blockly.serialization.workspaces.load(json, Blockly.getMainWorkspace());
                alert("Blocos carregados do arquivo!");
            } catch (error) {
                alert("Erro ao carregar os blocos: " + error.message);
            }
        };
        reader.readAsText(file);
    };
}
