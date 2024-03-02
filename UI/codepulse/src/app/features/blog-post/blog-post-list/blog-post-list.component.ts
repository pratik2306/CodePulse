import { Component, OnInit } from '@angular/core';
import { BlogpostService } from '../services/blogpost.service';
import { BlogPost } from '../models/blog-post.model';
import { Observable, Subscriber } from 'rxjs';

@Component({
  selector: 'app-blog-post-list',
  templateUrl: './blog-post-list.component.html',
  styleUrls: ['./blog-post-list.component.css']
})
export class BlogPostListComponent implements OnInit {

  blogPosts$?:Observable<BlogPost[]>
  
  constructor(private blogPostService:BlogpostService){

  }
  ngOnInit(): void {
    this.blogPosts$ = this.blogPostService.getAllBlogPosts()
  }
}
