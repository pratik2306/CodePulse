import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BlogpostService } from '../../blog-post/services/blogpost.service';
import { BlogPost } from '../../blog-post/models/blog-post.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-blog-details',
  templateUrl: './blog-details.component.html',
  styleUrls: ['./blog-details.component.css']
})
export class BlogDetailsComponent implements OnInit {

  url: string | null = null
  blogPost$?: Observable<BlogPost>
  constructor(private route: ActivatedRoute,
    private blogPostService: BlogpostService){

  }

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next : (param) =>{
        this.url = param.get('url')
        if(this.url){
          this.blogPost$ = this.blogPostService.getBlogPostByUrlHandle(this.url)
        }
      }
    })
  }

}
