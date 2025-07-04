// Access camera/microphone
navigator.mediaDevices.getUserMedia({ video: true, audio: true })
    .then(stream => {
        // Do something with the stream
    })
    .catch(error => {
        console.error('Error accessing media devices.', error);
    });