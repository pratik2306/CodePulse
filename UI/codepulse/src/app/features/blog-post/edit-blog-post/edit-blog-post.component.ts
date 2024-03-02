import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BlogpostService } from '../services/blogpost.service';
import { Observable, Subscription } from 'rxjs';
import { Category } from '../../category/models/category.model';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { UpdateBlogPostRequest } from '../models/update-blogpost-request.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-edit-blog-post',
  templateUrl: './edit-blog-post.component.html',
  styleUrls: ['./edit-blog-post.component.css']
})
export class EditBlogPostComponent implements OnInit, OnDestroy {

  id: string | null = null
  routSubscription?: Subscription
  updateblogpostSubscription?: Subscription
  deleteblogpostSubscription?: Subscription
  imageSelectionSubscription?: Subscription
  categories$?: Observable<Category[]>
  model?: BlogPost
  selectedCategories?: string[]
  isImageSelectorVisible : boolean = false

  constructor(private route: ActivatedRoute,
     private blogPostService:BlogpostService,
     private categoryService:CategoryService,
     private imageService: ImageService,
     private router: Router){}
  
  ngOnDestroy(): void {
    this.routSubscription?.unsubscribe()
    this.updateblogpostSubscription?.unsubscribe()
    this.deleteblogpostSubscription?.unsubscribe()
    this.imageSelectionSubscription?.unsubscribe()
  }

  ngOnInit(): void {
    this.routSubscription = this.route.paramMap.subscribe({
      next:(params) => {
        this.id = params.get('id');
        if(this.id){
          this.categories$ = this.categoryService.getAllCategories()
          this.blogPostService.getBlogPostById(this.id)
          .subscribe({
            next: (resp) =>{
              this.model = resp
              this.selectedCategories = resp.categories.map(x=> x.id)
            }
          })
        }
        this.imageSelectionSubscription = this.imageService.onSelectImage().subscribe({
          next: (res) =>{
            if(this.model){
              this.model.featuredImageUrl = res.url
              this.isImageSelectorVisible = false
            }
          }
        })
      }
    })
  }

  onFormSubmit(){
    
    if(this.model && this.id){
      var updateBlogPost: UpdateBlogPostRequest = {
        author: this.model.author,
        content: this.model.content,
        shortDescription: this.model.shortDescription,
        featuredImageUrl: this.model.featuredImageUrl,
        isVisible: this.model.isVisible,
        title: this.model.title,
        urlHandle:this.model.urlHandle,
        publishedDate: this.model.publishedDate,
        categories: this.selectedCategories ?? []
      }

      this.updateblogpostSubscription = this.blogPostService.updateBlogPost(this.id,updateBlogPost)
      .subscribe({
        next:(res)=>{
            this.router.navigateByUrl('/admin/blogposts')
        }
      })

    }
  }

  onDelete(){
    if(this.id){
      this.deleteblogpostSubscription = this.blogPostService.deleteBlogPost(this.id)
      .subscribe({
        next:(res)=>{
            this.router.navigateByUrl('/admin/blogposts')
        }
      })
    }
  }

  openImageSelector(): void{
    this.isImageSelectorVisible = true
  }

  closeImageSelector(): void{
    this.isImageSelectorVisible = false
  }
}
