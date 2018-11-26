// Write your JavaScript code.
window.onload = function () {
    const path = window.location.pathname;

    if (path == '/Beer' || path == '/Beer/') {
        return ExecMainBeer();
    } else if (~path.indexOf('Beer/Details')) {
        return ExecDetailBeer();
    }
}

const ExecMainBeer = () => {
    const beerGrid = document.querySelector('.beer-grid');
    const beers = beerGrid.querySelectorAll('.beer-item');
    const searchInput = document.querySelector('.beer-input input');

    searchInput.addEventListener('input', function (e) {
        if (this.value == '') {
            beerGrid.classList.remove('on-search');
            return;
        }

        beerGrid.classList.add('on-search');
        beers.forEach((beer) => {
            const hasInName = ~beer.querySelector('.title').textContent.toLowerCase().indexOf(this.value.toLowerCase());
            const hasInKind = ~beer.querySelector('.kind').textContent.toLowerCase().indexOf(this.value.toLowerCase());

            if (hasInKind || hasInName) {
                beer.classList.add('focused');
            } else {
                beer.classList.remove('focused');
            }
        });
    });
}

const ExecDetailBeer = () => {
    const {lat, long} = PLACES[0];

    if (lat && long) {
        const map = new google.maps.Map(document.getElementById('where-i-found'), {
            center: {lat, lng: long},
            zoom: 15
        });

        PLACES.forEach(place => {
            const marker = new google.maps.Marker({
                position: {lat: place.lat, lng: place.long},
                map: map,
                title: place.name
            });
        })
    }
}