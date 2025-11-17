export interface Meal {
  id: string;
  name: string;
  description: string;
  price: number;
  calories: number;
  proteins: number;
  carbohydrates: number;
  sugars: number;
  isVegan: boolean;
  photoPath: string;
  restaurantName: string;
}
