$(document).ready(() => {

    const params = new URLSearchParams(window.location.search);

    let sort = params.get("sort");
    let search = params.get("search");
    if (search != null) {
        $("#search").attr("value", search);
    }
    function buildNewUrl() {
        let newLocation = window.location.origin + window.location.pathname;
        if (search !== null) {
            newLocation += "?search=" + search;
        }
        if (search !== null && sort !== null) {
            newLocation += "&";
        }
        if (search === null && sort !== null) {
            newLocation += "?"
        }
        if (sort !== null) {
            newLocation += "sort=" + sort;
        }
        return newLocation;
    }

    $("#place-rating").click((e) => {
        if (sort === null) {
            sort = "asc";
        } else if (sort === "asc") {
            sort = "desc";
        } else {
            sort = null;
        }
        window.location.assign(buildNewUrl());
    });
    $("#search-btn").click(() => {
        search = $("#search")[0].value;
        if (search != "")
            window.location.assign(buildNewUrl());
    });
});