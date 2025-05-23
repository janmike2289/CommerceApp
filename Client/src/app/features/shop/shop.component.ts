import { Component, inject, OnInit } from '@angular/core';
import { Product } from '../../shared/models/products';
import { ShopService } from '../../core/services/shop.service';
import { ProductsItemComponent } from "./products-item/products-item.component";
import {MatDialog} from '@angular/material/dialog';
import {MatButton} from '@angular/material/button';
import {MatIcon} from '@angular/material/icon';
import { FiltersDialogComponent } from './filter-dialog/filter-dialog.component';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { ShopParams } from '../../shared/models/shopParams';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-shop',
  imports: [
    ProductsItemComponent,
    MatButton,
    MatIcon,
    MatMenu,
    MatSelectionList,
    MatListOption,
    MatMenuTrigger,
    MatPaginator,
    FormsModule,
    
],
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})

export class ShopComponent implements OnInit {

  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  // products: Product[] = [];
  products?: Pagination<Product>;
  sortOptions = [
    {name: 'Alpabetical', value: 'name'},
    {name: 'Price: Low-High', value: 'priceAsc'},
    {name: 'Price: High-Low', value: 'priceDesc'},
  ]

  shopParams = new ShopParams();
  pageSizeOptions = [5,10,15,20]

  ngOnInit(): void {
    this.initializeShop();
  }

  // initialize the methods to get the shop products, brands and types
  initializeShop() {
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getSortedProducts();
  }

  
  getSortedProducts(){
      this.shopService.getProducts(this.shopParams).subscribe({
      next: response => this.products = response,
      error: (error) => console.error(error),
      complete: () => console.log('complete')
    })
  }

  onSearchChange(){
    this.shopParams.pageNumber = 1;
    this.getSortedProducts();
  }

  //handle the paginator event
  handlePageEvent(event: PageEvent){
    this.shopParams.pageNumber = event.pageIndex + 1;
    this.shopParams.pageSize = event.pageSize;
    this.getSortedProducts();
    console.log("current page size " + this.shopParams.pageSize)
    console.log("current page index " + this.shopParams.pageNumber)
  }

  onSortChange(event: MatSelectionListChange) 
  {
    const selectedOption = event.options[0];
    if(selectedOption)
    {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getSortedProducts();
    }
  }

  openFiltersDialog(){

    const buttonElement = document.activeElement as HTMLElement; // Get the currently focused element
    buttonElement.blur(); // Remove focus from the button

    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });

    dialogRef.afterClosed().subscribe({
        next: result => {
          if (result) {
            console.log(result);
              this.shopParams.brands = result.selectedBrands;
              this.shopParams.types = result.selectedTypes;
              this.shopParams.pageNumber = 1;
              this.getSortedProducts();
          }
        }
      })
  }

}
