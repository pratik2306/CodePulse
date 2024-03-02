import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model'
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequest } from '../models/update-category-request-model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient, private cookieService: CookieService) { }


  getAllCategories(): Observable<Category[]>{
    return this.http.get<Category[]>(`${environment.apiBaseUrl}categories`)
  }

  getCategoryById(id: string):Observable<Category>{
    return this.http.get<Category>(`${environment.apiBaseUrl}categories/${id}`)
  }

  addCategory(mode:AddCategoryRequest):Observable<void> {
    return this.http.post<void>(`${environment.apiBaseUrl}categories`, mode)
  }

  updateCategory(id: string, model: UpdateCategoryRequest):Observable<Category>{
    return this.http.put<Category>(`${environment.apiBaseUrl}categories/${id}`, model)
  }

  deleteCategory(id: string): Observable<Category>{
    return this.http.delete<Category>(`${environment.apiBaseUrl}categories/${id}`)
  }
}
