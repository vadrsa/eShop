import { Brand } from ".";
import { Category } from "./category";

export class Product {
    id: number;
    name: string;
    price: number;
    image: string;
    imageBig: string;
    loaded: boolean;
    productCode:string;
    availability: string;
    brand: Brand;
    category: Category;
}