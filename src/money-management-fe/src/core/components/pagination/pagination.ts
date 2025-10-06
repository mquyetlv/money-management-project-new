import { Component, computed, input, Input, signal } from '@angular/core';

@Component({
  selector: 'mon-pagination',
  imports: [],
  templateUrl: './pagination.html',
  styleUrl: './pagination.css'
})
export class Pagination {
  size = input<number>(10);
  page = input<number>(0);
  total = input<number>(0);


  curentPage = signal(0);

  listPage = computed(() => {
    const result: any[] = [];

    const totalPage = Math.ceil(this.total() / this.size());
    if (totalPage <= 9) {
      for(let i = 1; i <= totalPage; ++i) {
        result.push(i);
      }
    }

    else {
      
    }


    return result;
  });
}
