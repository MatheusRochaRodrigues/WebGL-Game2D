// function enviarParaUnity(objeto, metodo, mensagem) {
//     if (unityInstance) {
//       unityInstance.SendMessage(objeto, metodo, mensagem);
//       console.log("foi.");
//     } else {
//       console.warn("Unity ainda não carregado.");
//       console.log(" n foi.");
//     }
//   }

// window.addEventListener('message', function(event) {
//         console.log('Mensagem recebida do pai:', event.data);
//         if (event.data.type === 'sendMessage') {
//             console.log('Mensagem válida recebida do pai:', event.data);
//             if (unityInstance) {
//                 unityInstance.SendMessage(event.data.target, event.data.method, event.data.message);
//             } else {
//                 console.error('Unity não está pronto para receber mensagens.');
//             }
//         }
// });

window.addEventListener('message', function(event) {
  if (event.data.type === 'sendMessage') {
      if (unityInstance) {
          unityInstance.SendMessage(event.data.target, event.data.method, event.data.message);
      } else {
          console.error('Unity não está pronto para receber mensagens.');
      }
  }
});

// setTimeout(() => enviarParaUnity("Circle_Outlined", "ReceiveMessage", "colorBlue"), 1000);

{/* <script src="/HandShake/myScripts.js"></script> */}

//backup de onde salvamos a INSTANCE UNITY

// let unityInstance = null; // Variável global para armazenar a instância

// createUnityInstance(document.querySelector("#unity-canvas"), {
//     dataUrl: "Build/Game.data",
//     frameworkUrl: "Build/Game.framework.js",
//     codeUrl: "Build/Game.wasm",
//     streamingAssetsUrl: "StreamingAssets",
//     companyName: "gamestdio",
//     productName: "UnityWebSockets",
//     productVersion: "1.0",
//     }).then((instance) => {
//       unityInstance = instance; // Armazena a instância do Unity quando carregado
//       console.log("Unity carregado!");
//     });



//tentativas

// window.getUnityInstance = function () {
//   return unityInstance;
// };
// window.parent.postMessage({ type: "unityInstance", instance: unityInstance }, "*");
