export class Category {
    id: number;
    name: string;
    children: Category[];
    productCount: number;
    isExpanded: Boolean;
} 