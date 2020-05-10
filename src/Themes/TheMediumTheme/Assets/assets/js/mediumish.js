$(function () {
    $('#mainNavigation').activateLinks();
    $('#searchButton').on('click', e => {
        e.preventDefault();
        $('#searchForm').submit();
    })
});
