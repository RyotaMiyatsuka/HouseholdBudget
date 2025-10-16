import { Component } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions, EventInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

@Component({
  selector: 'app-calender',
  imports: [FullCalendarModule],
  templateUrl: './calender.html',
  styleUrl: './calender.css'
})
export class Calender {
  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, interactionPlugin],
    initialView: 'dayGridMonth',
    locale: 'ja', // Japanese locale
    height: '100%',
    headerToolbar: {
      left: 'prev,next today',
      center: 'title',
      right: 'dayGridMonth,dayGridWeek'
    },
    events: this.getExpenditureEvents(),
    dateClick: this.handleDateClick.bind(this),
    eventClick: this.handleEventClick.bind(this)
  };

  // Sample expenditure data - replace with actual data from your backend
  getExpenditureEvents(): EventInput[] {
    return [
      {
        title: '¥1,500',
        date: '2025-10-15',
        backgroundColor: '#FF6B6B',
        extendedProps: {
          amount: 1500,
          genre: '食費'
        }
      },
      {
        title: '¥3,200',
        date: '2025-10-14',
        backgroundColor: '#4ECDC4',
        extendedProps: {
          amount: 3200,
          genre: '交通費'
        }
      }
    ];
  }

  handleDateClick(arg: any) {
    console.log('Date clicked:', arg.dateStr);
    // Navigate to input screen or show details
  }

  handleEventClick(arg: any) {
    console.log('Event clicked:', arg.event.extendedProps);
    // Show expenditure details
  }
}
