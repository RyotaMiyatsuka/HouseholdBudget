import { Component, signal, computed } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions, EventInput } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';

@Component({
  selector: 'app-calender',
  imports: [FullCalendarModule],
  templateUrl: './calender.html',
  styleUrl: './calender.scss'
})
export class Calender {
  // Signal for expenditure events
  expenditureEvents = signal<EventInput[]>([
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
  ]);

  // Computed signal for calendar options
  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, interactionPlugin],
    initialView: 'dayGridMonth',
    locale: 'ja', // Japanese locale
    height: '100%',
    headerToolbar: {
      left: '',
      center: 'prev,title,next',
      right: 'dayGridMonth,dayGridWeek'
    },
    events: this.expenditureEvents(),
    dateClick: this.handleDateClick.bind(this),
    eventClick: this.handleEventClick.bind(this)
  };

  handleDateClick(arg: any) {
    console.log('Date clicked:', arg.dateStr);
    // Navigate to input screen or show details
  }

  handleEventClick(arg: any) {
    console.log('Event clicked:', arg.event.extendedProps);
    // Show expenditure details
  }

  // Method to update events dynamically
  updateEvents(newEvents: EventInput[]) {
    this.expenditureEvents.set(newEvents);
  }

  // Method to add a single event
  addEvent(event: EventInput) {
    this.expenditureEvents.update(events => [...events, event]);
  }
}
