function calculateTotal() {
    var total = 0;

    document.querySelectorAll('.chambre-checkbox:checked').forEach(function (checkbox) {
        total += parseFloat(checkbox.getAttribute('data-price'));
    });

    document.querySelectorAll('.service-checkbox:checked').forEach(function (checkbox) {
        total += parseFloat(checkbox.getAttribute('data-price'));
    });

    document.getElementById('total-price').textContent = total.toFixed(2);
}

document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('.chambre-checkbox').forEach(function (checkbox) {
        checkbox.addEventListener('change', calculateTotal);
    });

    document.querySelectorAll('.service-checkbox').forEach(function (checkbox) {
        checkbox.addEventListener('change', calculateTotal);
    });

    calculateTotal();
});
