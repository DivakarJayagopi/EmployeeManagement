function LoadPieChart(TodayBirthdayCount, TodayLeavesCount, TodayWorkAniversayCount, RemainingCount) {
    const employeeStatusPie = document.getElementById('employeeStatusPie');
    new Chart(employeeStatusPie, {
        type: 'pie',
        data: {
            labels: ['Birthday', 'Leave', 'Anniversary', 'Remaining'],
            datasets: [{
                label: '# of Votes',
                data: [TodayBirthdayCount, TodayLeavesCount, TodayWorkAniversayCount, RemainingCount],
                backgroundColor: [
                    'Red',
                    'Orange',
                    'Green',
                    'Blue'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function LoadBarChart(TodayBirthdayCount, TodayLeavesCount, TodayWorkAniversayCount, RemainingCount) {
    const employeeStatusBar = document.getElementById('employeeStatusBar');
    new Chart(employeeStatusBar, {
        type: 'bar',
        data: {
            labels: ['Birthday', 'Leave', 'Anniversary', 'Remaining'],
            datasets: [{
                label: 'Employee Status',
                data: [TodayBirthdayCount, TodayLeavesCount, TodayWorkAniversayCount, RemainingCount],
                backgroundColor: [
                    'Purple',
                    'Gray',
                    'Green',
                    'Black'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function LoadPieChartNormal(TotalLeaveAvailableCount, TotalLeaveTakenCount) {
    const employeeStatusPieNormal = document.getElementById('employeeStatusPieNormal');
    new Chart(employeeStatusPieNormal, {
        type: 'pie',
        data: {
            labels: ['Available', 'Taken'],
            datasets: [{
                label: '# of Votes',
                data: [TotalLeaveAvailableCount, TotalLeaveTakenCount],
                backgroundColor: [
                    'Red',
                    'Orange'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function LoadBarChartNormal(TotalLeaveAvailableCount, TotalLeaveTakenCount) {
    const employeeStatusBarNormal = document.getElementById('employeeStatusBarNormal');
    new Chart(employeeStatusBarNormal, {
        type: 'bar',
        data: {
            labels: ['Available', 'Taken'],
            datasets: [{
                label: 'Leave Status',
                data: [TotalLeaveAvailableCount, TotalLeaveTakenCount],
                backgroundColor: [
                    'Red',
                    'Orange'
                ],
                borderColor: [
                    'rgba(255, 99, 132, 1)',
                    'rgba(54, 162, 235, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

if (IsAdmin == "1") {
    var TotalEmployeeCount = $(".TotalEmployeeCount").text();
    var TodayBirthdayCount = $(".TodayBirthdayCount").text();
    var TodayLeavesCount = $(".TodayLeavesCount").text();
    var TodayWorkAniversayCount = $(".TodayWorkAniversayCount").text();

    LoadPieChart(TodayBirthdayCount, TodayLeavesCount, TodayWorkAniversayCount, RemainingCount);
    LoadBarChart(TodayBirthdayCount, TodayLeavesCount, TodayWorkAniversayCount, RemainingCount);
} else {
    var TotalLeaveAvailableCount = $(".TotalLeaveAvailableCount").text();
    var TotalLeaveTakenCount = $(".TotalLeaveTakenCount").text();
    LoadPieChartNormal(TotalLeaveAvailableCount, TotalLeaveTakenCount);
    LoadBarChartNormal(TotalLeaveAvailableCount, TotalLeaveTakenCount);
}

