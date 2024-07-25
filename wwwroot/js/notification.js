const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceivePostNotification", function (title) {
    console.log('Notification received:', title);  // Log to verify if event is received
    alert(`منشور جديد: ${title}`);
});

connection.start()
    .then(() => console.log('SignalR Connected'))
    .catch(function (err) {
        console.error('SignalR Connection Error:', err.toString());
    });