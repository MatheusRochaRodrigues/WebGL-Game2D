
// Verificar se o iframe está carregado
document.getElementById('unityIframe').addEventListener('load', function() {
  console.log('Iframe carregado');
});

// Enviar mensagem para o Unity no iframe
// document.getElementById('sendMessageButton').addEventListener('click', function() {
//   const iframe = document.getElementById('unityIframe');
//   const message = {
//       type: 'sendMessage',
//       target: 'Circle_Outlined', // Nome do objeto no Unity
//       method: 'ReceiveMessage', // Nome do método no Unity
//       message: 'up'
//   };
//   console.log('Enviando mensagem:', message);
//   iframe.contentWindow.postMessage(message, '*');
// });

// // Receber mensagem do Unity no iframe
// window.addEventListener('message', function(event) {
//   console.log('Mensagem recebida do iframe:', event.data);
// });






























// document.getElementById('unityIframe').addEventListener('load', function() {
//   console.log('Iframe carregado');
// });


// // Enviar mensagem para o Unity no iframe
// document.getElementById('bt1').addEventListener('click', function() {
//   const iframe = document.getElementById('unityIframe');
//   const message = {
//       type: 'sendMessage',
//       target: 'Sphere', // Nome do objeto no Unity
//       method: 'ReceiveMessage', // Nome do método no Unity
//       message: 'Olá do pai!'
//   };
//   console.log('Enviando mensagem:', message);
//   iframe.contentWindow.postMessage(message, '*');
// });














// function sendMessageToUnity1() {
//     sendMessageToUnity('Sphere', 'ReceiveMessage', 'testando tcc')
//   }

//   function sendMessageToUnity(objectName, methodName, messages) {
//     const iframe = document.getElementById('unityIframe');
//     if (iframe && iframe.contentWindow) {
//         iframe.contentWindow.postMessage({
//             type: 'sendMessage',
//             target: objectName,
//             method: methodName,
//             message: messages
//         }, '*');
//         console.log('Mensagem enviada para o iframe:', {
//           type: 'sendMessage',
//           target: objectName,
//           method: methodName,
//           message: messages
//       });
//       //eval("document.getElementById('unityIframe').contentWindow.sendMessage('Sphere', 'ReceiveMessage', 'testando tcc')")
//     } else {
//         console.error('Iframe não encontrado ou não carregado.');
//     }
//   }

//   document.getElementById('unityIframe').addEventListener('load', function() {
//     console.log('Iframe carregado');
//   });

// //document.querySelector('#GAME').addEventListener('click', sendMessageToUnity1);



// // window.addEventListener('message', function(event) {
// //   console.log('Mensagem recebida do iframe:', event.data);

// //   // Enviando uma mensagem de volta para a página principal
// //   event.source.postMessage('Mensagem recebida no iframe', event.origin);
// // });

 
// document.getElementById('GAME').addEventListener('click', function() {
//     console.log('entrou');
//   const iframe = document.getElementById('unityIframe');
//   const message = {
//       type: 'sayHello',
//       content: 'Olá do pai!'
//   };
//   iframe.contentWindow.postMessage(message, '*');

  
//     document.getElementById('unityIframe').contentWindow.targetFunction();
// });


// window.addEventListener('message', function(event) {
//     console.log('saiu');
//   console.log('Mensagem recebida do iframe:', event.data);
// });
