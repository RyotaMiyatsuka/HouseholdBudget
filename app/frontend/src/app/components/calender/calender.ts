import { Component, signal, inject, OnInit, effect } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions, EventInput, DatesSetArg } from '@fullcalendar/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import { TransactionsService } from '../../services/transaction/transactions.service';
import { Transaction } from '../../models/transaction.model';

@Component({
  selector: 'app-calender',
  imports: [FullCalendarModule],
  templateUrl: './calender.html',
  styleUrl: './calender.scss'
})
export class Calender implements OnInit {
  private transactionsService = inject(TransactionsService);

  // Signal for current year and month
  currentYear = signal<number>(new Date().getFullYear());
  currentMonth = signal<number>(new Date().getMonth() + 1);

  // Signal for transactions
  transactions = signal<Transaction[]>([]);

  // Signal for calendar events
  calendarEvents = signal<EventInput[]>([]);

  // Calendar options
  calendarOptions: CalendarOptions = {
    plugins: [dayGridPlugin, interactionPlugin],
    initialView: 'dayGridMonth',
    locale: 'ja',
    height: '100%',
    headerToolbar: {
      left: '',
      center: 'prev,title,next',
      right: 'dayGridMonth,dayGridWeek'
    },
    events: [],
    dateClick: this.handleDateClick.bind(this),
    eventClick: this.handleEventClick.bind(this),
    datesSet: this.handleDatesSet.bind(this)
  };

  constructor() {
    // Update calendar events when transactions change
    effect(() => {
      const events = this.transformTransactionsToEvents(this.transactions());
      this.calendarEvents.set(events);
      this.calendarOptions = {
        ...this.calendarOptions,
        events: events
      };
    });
  }

  ngOnInit() {
    this.loadTransactions();
  }

  /**
   * Load transactions for the current month
   */
  loadTransactions() {
    const year = this.currentYear();
    const month = this.currentMonth();

    this.transactionsService.getTransactionsByMonth(year, month).subscribe({
      next: (transactions) => {
        this.transactions.set(transactions);
      },
      error: (error) => {
        console.error('Failed to load transactions:', error);
        this.transactions.set([]);
      }
    });
  }

  /**
   * Handle calendar date range changes (month navigation)
   */
  handleDatesSet(arg: DatesSetArg) {
    const startDate = new Date(arg.start);
    // Get the middle of the visible range to determine the current month
    const middleDate = new Date(arg.start.getTime() + (arg.end.getTime() - arg.start.getTime()) / 2);

    const newYear = middleDate.getFullYear();
    const newMonth = middleDate.getMonth() + 1;

    // Only reload if year or month changed
    if (newYear !== this.currentYear() || newMonth !== this.currentMonth()) {
      this.currentYear.set(newYear);
      this.currentMonth.set(newMonth);
      this.loadTransactions();
    }
  }

  /**
   * Transform transactions to FullCalendar events
   */
  transformTransactionsToEvents(transactions: Transaction[]): EventInput[] {
    return transactions.map(transaction => {
      const isIncome = transaction.transactionType === 'income';
      const backgroundColor = isIncome ? '#4CAF50' : '#2196F3'; // Green for income, Blue for expense
      const prefix = isIncome ? '+' : '-';

      return {
        id: transaction.id,
        title: `${prefix}¥${transaction.amount.toLocaleString()}`,
        date: transaction.date,
        backgroundColor,
        borderColor: backgroundColor,
        extendedProps: {
          amount: transaction.amount,
          transactionType: transaction.transactionType,
          categoryId: transaction.categoryId,
          memo: transaction.memo,
          place: transaction.place
        }
      };
    });
  }

  handleDateClick(arg: any) {
    console.log('Date clicked:', arg.dateStr);
    // TODO: Navigate to input screen with pre-filled date
  }

  handleEventClick(arg: any) {
    console.log('Transaction clicked:', arg.event.extendedProps);
    // TODO: Show transaction details modal or navigate to edit screen
  }
}
