
$(document).on('click', '.AddEmployeePaySlip', function () {
    var EmployeeId = $("#EmployeesList").val();

    var Basic = $(".Basic").val().trim();
    var DA = $(".DA").val().trim();
    var HRA = $(".HRA").val().trim();
    var MedicalAllowances = $(".MedicalAllowances").val().trim();
    var ConveyanceCharges = $(".ConveyanceCharges").val().trim();
    var SpecialAllowances = $(".SpecialAllowances").val().trim();

    var IncomeTax = $(".IncomeTax").val().trim();
    var EducationalCess = $(".EducationalCess").val().trim();
    var LOP = $(".LOP").val().trim();

    var PaidMonth = $(".PaidMonth").val();

    if (Basic == "" || DA == "" || HRA == "" || MedicalAllowances == "" || ConveyanceCharges == "" || SpecialAllowances == "" || IncomeTax == "" || EducationalCess == "" || LOP == "") {

        $(".customErrorMessageAddEmployee").text("All fields are Mandatory");

        if (Basic == "") $(".Basic").addClass("form-error");
        else $(".Basic").removeClass("form-error");

        if (DA == "") $(".DA").addClass("form-error");
        else $(".DA").removeClass("form-error");

        if (HRA == "") $(".HRA").addClass("form-error");
        else $(".HRA").removeClass("form-error");

        if (MedicalAllowances == "") $(".MedicalAllowances").addClass("form-error");
        else $(".MedicalAllowances").removeClass("form-error");

        if (ConveyanceCharges == "") $(".ConveyanceCharges").addClass("form-error");
        else $(".ConveyanceCharges").removeClass("form-error");

        if (SpecialAllowances == "") $(".SpecialAllowances").addClass("form-error");
        else $(".SpecialAllowances").removeClass("form-error");

        if (IncomeTax == "") $(".IncomeTax").addClass("form-error");
        else $(".IncomeTax").removeClass("form-error");

        if (EducationalCess == "") $(".EducationalCess").addClass("form-error");
        else $(".EducationalCess").removeClass("form-error");

        if (LOP == "") $(".LOP").addClass("form-error");
        else $(".LOP").removeClass("form-error");
    } else {
        $("input[type='number']").removeClass("form-error");

        $(".customErrorMessageAddLeaveRequest").text("");
        var data = '{EmployeeId:"' + EmployeeId + '", Basic:' + Basic + ', DA:' + DA + ', HRA:' + HRA + ', MedicalAllowances:' + MedicalAllowances + ', ConveyanceCharges:' + ConveyanceCharges + ', SpecialAllowances:' + SpecialAllowances + ', IncomeTax:' + IncomeTax + ', EducationalCess:' + EducationalCess + ', LOP:' + LOP + ', PaidMonth : "' + PaidMonth + '"}';
        handleAjaxRequest(null, true, "/Method/AddPaySlip", data, "CallBackAddPaySlip");
    }
});

function CallBackAddPaySlip(responseData) {
    if (responseData.message.status == "success") {
        $("input[type='number']").val("");
        swal({
            title: "Success",
            text: "PaySlip Added Successfully",
            icon: "success",
        });
    }
    else {
        $(".customErrorMessageAddLeaveRequest").text("Error on Adding leave request, Try agin in few min");
    }
}

function SalaryCalculation() {
    var Basic = $(".Basic").val().trim();
    var DA = $(".DA").val().trim();
    var HRA = $(".HRA").val().trim();
    var MedicalAllowances = $(".MedicalAllowances").val().trim();
    var ConveyanceCharges = $(".ConveyanceCharges").val().trim();
    var SpecialAllowances = $(".SpecialAllowances").val().trim();

    var IncomeTax = $(".IncomeTax").val().trim();
    var EducationalCess = $(".EducationalCess").val().trim();
    var LOP = $(".LOP").val().trim();

    if (Basic == "") Basic = 0;
    if (DA == "") DA = 0;
    if (HRA == "") HRA = 0;
    if (MedicalAllowances == "") MedicalAllowances = 0;
    if (ConveyanceCharges == "") ConveyanceCharges = 0;
    if (SpecialAllowances == "") SpecialAllowances = 0;

    if (IncomeTax == "") IncomeTax = 0;
    if (EducationalCess == "") EducationalCess = 0;
    if (LOP == "") LOP = 0;

    var TotalEarnings = parseInt(Basic) + parseInt(DA) + parseInt(HRA) + parseInt(MedicalAllowances) + parseInt(ConveyanceCharges) + parseInt(SpecialAllowances);
    var TotalDeduction = parseInt(IncomeTax) + parseInt(EducationalCess) + parseInt(LOP);

    var NetPay = TotalEarnings - TotalDeduction;

    $(".TotalEarnings").text(TotalEarnings);
    $(".TotalDeduction").text(TotalDeduction);
    $(".NetPay").text(NetPay);
}

$(document).on('click', '.SelectPaySlipMonth', function () {
    var EmployeeId = $("#EmployeesList").val();
    var SelectedMonth = $(".PaidMonth").val();

    if (EmployeeId == "undefined" || EmployeeId == null || EmployeeId == "") {
        EmployeeId = "";
    }
    $(".noDataFound").hide();
    $(".SalaryInfo").hide();
    var data = '{EmployeeId:"' + EmployeeId + '", SelectedMonth:"' + SelectedMonth + '"}';
    handleAjaxRequest(null, true, "/Method/GetpaySlipByEmployeeId", data, "CallBackGetpaySlipByEmployeeId");
});

function CallBackGetpaySlipByEmployeeId(responseData) {
    if (responseData.message.status == "success") {
        var PaySlipInfo = responseData.message.PaySlipInfo;
        var EmployeeInfo = responseData.message.EmployeeInfo;

        if (PaySlipInfo.Basic == 0 && PaySlipInfo.DA == 0 && PaySlipInfo.HRA == 0 && PaySlipInfo.MedicalAllowances == 0 && PaySlipInfo.ConveyanceCharges == 0 && PaySlipInfo.SpecialAllowances == 0 && PaySlipInfo.IncomeTax == 0 && PaySlipInfo.EducationalCess == 0 && PaySlipInfo.LOP == 0) {
            $(".noDataFound").show();
            $(".SalaryInfo").hide();
        } else {
            $(".Name").text(EmployeeInfo.Name);
            $(".Role").text(EmployeeInfo.Role);
            $(".Team").text(EmployeeInfo.Team);
            $(".doj").text(responseData.message.EmployeeDateOfJoining);

            $(".Basic").text(PaySlipInfo.Basic);
            $(".DA").text(PaySlipInfo.DA);
            $(".HRA").text(PaySlipInfo.HRA);
            $(".MedicalAllowances").text(PaySlipInfo.MedicalAllowances);
            $(".ConveyanceCharges").text(PaySlipInfo.ConveyanceCharges);
            $(".SpecialAllowances").text(PaySlipInfo.SpecialAllowances);

            $(".IncomeTax").text(PaySlipInfo.IncomeTax);
            $(".EducationalCess").text(PaySlipInfo.EducationalCess);
            $(".LOP").text(PaySlipInfo.LOP);



            if (PaySlipInfo.Basic == "") PaySlipInfo.Basic = 0;
            if (PaySlipInfo.DA == "") PaySlipInfo.DA = 0;
            if (PaySlipInfo.HRA == "") PaySlipInfo.HRA = 0;
            if (PaySlipInfo.MedicalAllowances == "") PaySlipInfo.MedicalAllowances = 0;
            if (PaySlipInfo.ConveyanceCharges == "") PaySlipInfo.ConveyanceCharges = 0;
            if (PaySlipInfo.SpecialAllowances == "") PaySlipInfo.SpecialAllowances = 0;

            if (PaySlipInfo.IncomeTax == "") PaySlipInfo.IncomeTax = 0;
            if (PaySlipInfo.EducationalCess == "") PaySlipInfo.EducationalCess = 0;
            if (PaySlipInfo.LOP == "") PaySlipInfo.LOP = 0;

            var TotalEarnings = parseInt(PaySlipInfo.Basic) + parseInt(PaySlipInfo.DA) + parseInt(PaySlipInfo.HRA) + parseInt(PaySlipInfo.MedicalAllowances) + parseInt(PaySlipInfo.ConveyanceCharges) + parseInt(PaySlipInfo.SpecialAllowances);
            var TotalDeduction = parseInt(PaySlipInfo.IncomeTax) + parseInt(PaySlipInfo.EducationalCess) + parseInt(PaySlipInfo.LOP);

            var NetPay = TotalEarnings - TotalDeduction;

            $(".TotalEarnings").text(TotalEarnings);
            $(".TotalDeduction").text(TotalDeduction);
            $(".NetPay").text(NetPay);

            $(".SalaryInfo").show();
            $(".noDataFound").hide();            
        }

        
    }
    else {
        $(".customErrorMessageAddLeaveRequest").text("Error on Adding leave request, Try agin in few min");
    }
}