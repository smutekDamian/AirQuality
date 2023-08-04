function initializeAutocomplete() {
    const input = document.getElementById("autocomplete");
    const autocomplete = new google.maps.places.Autocomplete(input);
    google.maps.event.addListener(autocomplete, "place_changed", function () {
        const place = autocomplete.getPlace();
        document.getElementById("localization").value = place.name;
        document.getElementById("lat").value = place.geometry.location.lat().toFixed(8);
        document.getElementById("lng").value = place.geometry.location.lng().toFixed(8);
    });
}

function onCheckQualityFormSubmit(e) {
    e.preventDefault();

    const url = e.target.action;
    const payload = new FormData(e.target);
    const resultsContainer = document.getElementById("airQualityResults");

    fetch(url, {
        method: "post",
        body: payload
    })
    .then(res => res.text())
    .then(html => resultsContainer.innerHTML = html);
}

const checkQualityForm = document.getElementById("checkQuality");

if (checkQualityForm) {
    checkQualityForm.addEventListener("submit", onCheckQualityFormSubmit);
}

