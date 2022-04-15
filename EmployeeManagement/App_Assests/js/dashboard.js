function LoadPieChart() {
    const employeeStatusPie = document.getElementById('employeeStatusPie');
    new Chart(employeeStatusPie, {
        type: 'pie',
        data: {
            labels: ['Birthday', 'Leave', 'Anniversary', 'No Value'],
            datasets: [{
                label: '# of Votes',
                data: [8, 10, 2, 59],
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

function LoadBarChart() {
    const employeeStatusBar = document.getElementById('employeeStatusBar');
    new Chart(employeeStatusBar, {
        type: 'bar',
        data: {
            labels: ['Birthday', 'Leave', 'Anniversary', 'No Value'],
            datasets: [{
                label: 'Employee Status',
                data: [8, 10, 2, 59],
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

function LoadPieChartNormal() {
    const employeeStatusPieNormal = document.getElementById('employeeStatusPieNormal');
    new Chart(employeeStatusPieNormal, {
        type: 'pie',
        data: {
            labels: ['Available', 'Taken'],
            datasets: [{
                label: '# of Votes',
                data: [8, 2],
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

function LoadBarChartNormal() {
    const employeeStatusBarNormal = document.getElementById('employeeStatusBarNormal');
    new Chart(employeeStatusBarNormal, {
        type: 'bar',
        data: {
            labels: ['Available', 'Taken'],
            datasets: [{
                label: 'Leave Status',
                data: [8, 2],
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

if (IsAdmin == "1" || IsAdmin == 1) {
    LoadPieChart();
    LoadBarChart();
} else {
    LoadPieChartNormal();
    LoadBarChartNormal();
}

