import { Component, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { Chart, CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, BarController } from 'chart.js';

// Register Chart.js components
Chart.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend, BarController);

@Component({
  selector: 'app-report',
  imports: [],
  templateUrl: './report.html',
  styleUrl: './report.scss'
})
export class Report implements AfterViewInit {
  @ViewChild('expenditureCanvas') expenditureCanvas!: ElementRef;
  chartInstance: Chart | undefined;

  // Dummy expenditure data
  expenditureData = {
    labels: ['1月', '2月', '3月', '4月', '5月', '6月'],
    amounts: [45000, 12000, 15000, 8000, 20000, 5000]
  };

  ngAfterViewInit() {
    const ctx = this.expenditureCanvas.nativeElement.getContext('2d');
    if (ctx) {
      this.chartInstance = new Chart(ctx, {
        type: 'bar',
        data: {
          labels: this.expenditureData.labels,
          datasets: [{
            label: '支出額 (円)',
            data: this.expenditureData.amounts,
            backgroundColor: 'rgba(59, 130, 246, 0.8)',
            borderColor: 'rgba(59, 130, 246, 1)',
            borderWidth: 1
          }]
        },
        options: {
          responsive: true,
          maintainAspectRatio: false,
          plugins: {
            legend: {
              labels: {
                color: '#1f2937',
                font: {
                  size: 14
                }
              }
            },
            tooltip: {
              backgroundColor: 'rgba(255, 255, 255, 0.95)',
              titleColor: '#1f2937',
              bodyColor: '#1f2937',
              borderColor: '#e5e7eb',
              borderWidth: 1
            }
          },
          scales: {
            y: {
              beginAtZero: true,
              ticks: {
                color: '#6b7280',
                callback: function (value) {
                  return '¥' + value.toLocaleString();
                }
              },
              grid: {
                color: '#f3f4f6'
              }
            },
            x: {
              ticks: {
                color: '#6b7280'
              },
              grid: {
                color: '#f3f4f6'
              }
            }
          }
        }
      });
    }
  }
}
