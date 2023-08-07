function initializeAutocomplete() {
    const input = document.getElementById("autocomplete");
    const autocomplete = new google.maps.places.Autocomplete(input);
    google.maps.event.addListener(autocomplete, "place_changed", function () {
        const place = autocomplete.getPlace();
        document.getElementById("localization").value = place.name;
        document.getElementById("lat").value = place.geometry.location.lat().toFixed(8);
        document.getElementById("lng").value = place.geometry.location.lng().toFixed(8);

        loadResults();
    });
}

function toggleLoadingSpinner() {
    const loadingSpinner = document.getElementById("loadingSpinner");
    const displayNoneClass = "d-none";

    if (loadingSpinner.classList.contains(displayNoneClass)) {
        loadingSpinner.classList.remove(displayNoneClass);
    } else {
        loadingSpinner.classList.add(displayNoneClass);
    }
}

function resetForm() {
    document.getElementById("localization").value = "";
    document.getElementById("lat").value = "";
    document.getElementById("lng").value = "";
}

function onCheckQualityFormSubmit(e) {
    e.preventDefault();
    loadResults();
}

function loadResults() {
    toggleLoadingSpinner();
    const form = document.getElementById("checkQuality");
    const resultsContainer = document.getElementById("airQualityResults");
    resultsContainer.innerHTML = "";

    const url = form.action;
    const payload = new FormData(form);

    setTimeout(function () {
        fetch(url, {
                method: "post",
                body: payload
            })
            .then(res => res.text())
            .then(html => {
                resultsContainer.innerHTML = html;
                toggleLoadingSpinner();
                resetForm();
            });
    }, 1000);
};

const checkQualityForm = document.getElementById("checkQuality");

if (checkQualityForm) {
    checkQualityForm.addEventListener("submit", onCheckQualityFormSubmit);
}

