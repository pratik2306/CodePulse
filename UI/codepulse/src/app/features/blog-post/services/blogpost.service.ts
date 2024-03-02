import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AddBlogPostRequest } from '../models/add-blogpost-request.model';
import { Observable } from 'rxjs';
import { BlogPost } from '../models/blog-post.model';
import { environment } from 'src/environments/environment.development';
import { UpdateBlogPostRequest } from '../models/update-blogpost-request.model';

@Injectable({
  providedIn: 'root'
})
export class BlogpostService {

  constructor(private http: HttpClient) { }

  createblogPost(model:AddBlogPostRequest):Observable<BlogPost>{
    return this.http.post<BlogPost>(`${environment.apiBaseUrl}blogposts`,model)
  }

  getAllBlogPosts():Observable<BlogPost[]>{
    return this.http.get<BlogPost[]>(`${environment.apiBaseUrl}blogposts`)
  }

  getBlogPostById(id: string):Observable<BlogPost>{
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}blogposts/${id}`)
  }

  updateBlogPost(id: string, model: UpdateBlogPostRequest):Observable<BlogPost>{
    return this.http.put<BlogPost>(`${environment.apiBaseUrl}blogposts/${id}`,model)
  }

  deleteBlogPost(id:string):Observable<BlogPost>{
    return this.http.delete<BlogPost>(`${environment.apiBaseUrl}blogposts/${id}`)
  }

  getBlogPostByUrlHandle(urlHandle: string):Observable<BlogPost>{
    return this.http.get<BlogPost>(`${environment.apiBaseUrl}blogposts/${urlHandle}`)
  }
}
