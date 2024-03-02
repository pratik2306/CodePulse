import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPostRequest } from '../models/add-blogpost-request.model';
import { BlogpostService } from '../services/blogpost.service';
import { Observable, Subscriber, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { Category } from '../../category/models/category.model';
import { CategoryService } from '../../category/services/category.service';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blog-post',
  templateUrl: './add-blog-post.component.html',
  styleUrls: ['./add-blog-post.component.css']
})
export class AddBlogPostComponent implements OnDestroy, OnInit {

  model:AddBlogPostRequest
  createblogPostSubscrition? : Subscription
  selectedImageSubscription?: Subscription
  categories$?:Observable<Category[]>
  isImageSelectorVisible : boolean = false
  constructor(private blogPostService: BlogpostService,
     private router:Router,
     private categoryService:CategoryService,
     private imageService: ImageService){
    this.model = {
      title:'',
      urlHandle: '',
      shortDescription:'',
      content:'',
      isVisible: true,
      featuredImageUrl: '',
      author:'',
      publishedDate: new Date(),
      categories:[]
    }
  }

  ngOnInit(): void {
    this.categories$ = this.categoryService.getAllCategories()
    this.selectedImageSubscription = this.imageService.onSelectImage().subscribe({
      next:(res) => {
        this.model.featuredImageUrl = res.url,
        this.isImageSelectorVisible = false;
      }
    })
  }

  ngOnDestroy(): void {
    this.createblogPostSubscrition?.unsubscribe()
    this.selectedImageSubscription?.unsubscribe()
  }

  onFormSubmit(){
    this.createblogPostSubscrition = this.blogPostService.createblogPost(this.model)
    .subscribe({
      next:(res)=>{
        console.log(res)
        this.router.navigateByUrl('/admin/blogposts')
      }
    })
  }

  openImageSelector(): void{
    this.isImageSelectorVisible = true
  }

  closeImageSelector(): void{
    this.isImageSelectorVisible = false
  }
}
