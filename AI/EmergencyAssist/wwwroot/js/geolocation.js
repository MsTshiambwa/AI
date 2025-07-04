// Get user's location
navigator.geolocation.getCurrentPosition(position => {
    console.log(position.coords.latitude, position.coords.longitude);
}, error => {
    console.error('Error getting location.', error);
});