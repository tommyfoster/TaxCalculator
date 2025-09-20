$("#calcForm").on("submit", function (e) {
    e.preventDefault();
    var salary = $("#salary").val();

    $.ajax({
        url: "/api/calculate",
        method: "GET",
        data: { gross: salary },
        success: function (data) {

            $("#grossAnnualSalary").text(data.grossAnnualSalary);
            $("#grossMonthlySalary").text(data.grossMonthlySalary.toFixed(2));
            $("#netAnnualSalary").text(data.netAnnualSalary);
            $("#netMonthlySalary").text(data.netMonthlySalary.toFixed(2));
            $("#annualTax").text(data.annualTax);
            $("#monthlyTax").text(data.monthlyTax.toFixed(2));

            // Make visible
            $("#resultsSection").show();
        },
        error: function (xhr, status, error) {
            alert("Error: " + xhr.responseText);
        }
    });
});