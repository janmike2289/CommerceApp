import { Component, Input } from '@angular/core';
import { MatCard, MatCardActions, MatCardContent } from '@angular/material/card';
import { Product } from '../../../shared/models/products';
import { CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-products-item',
  imports: [
    MatCard,
    MatCardContent,
    CurrencyPipe,
    MatCardActions,
    MatButton,
    MatIcon
  ],
  templateUrl: './products-item.component.html',
  styleUrl: './products-item.component.scss'
})
export class ProductsItemComponent {
  @Input() products?: Product;
}
