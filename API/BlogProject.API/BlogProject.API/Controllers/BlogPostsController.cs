﻿using Azure.Core;
using BlogProject.API.Models.Domain;
using BlogProject.API.Models.DTO;
using BlogProject.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostsController(IBlogPostRepository blogPostRepository, 
            ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }

       

        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeatureImageUrl = request.FeatureImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
               
            };

            foreach (var categoryGuid   in request.Categories) 
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if(existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }


           blogPost = await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto 
                { 
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle  = x.UrlHandle
                }).ToList()
               
            };


            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
           var blogPosts =  await blogPostRepository.GetAllAsync();

            var response = new List<BlogPostDto>();
            foreach(var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeatureImageUrl = blogPost.FeatureImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishDate = blogPost.PublishDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    UrlHandle = blogPost.UrlHandle,
                    Categories = blogPost.Categories.Select(x => new CategoryDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle
                    }).ToList()
                });
              
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetBlogPostById([FromRoute] Guid id)
        {
           var blogPost = await blogPostRepository.GetByIdAsync(id);

            if(blogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            };


            return Ok(response);
        }


        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateBlogPostbyId([FromRoute] Guid id, UpdateBlogPostRequestDto request)
        {
            var blogPost = new BlogPost
            {
                Id = id,
                Author = request.Author,
                Content = request.Content,
                FeatureImageUrl = request.FeatureImageUrl,
                IsVisible = request.IsVisible,
                PublishDate = request.PublishDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()

            };

            foreach (var categoryGuid in request.Categories)
            {
                var existingCategory = await categoryRepository.GetById(categoryGuid);
                if (existingCategory != null)
                {
                    blogPost.Categories.Add(existingCategory);
                }
            }

           var updatedBlogPost =  await blogPostRepository.UpdateAsync(blogPost);

            if(updatedBlogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeatureImageUrl = blogPost.FeatureImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishDate = blogPost.PublishDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                Categories = blogPost.Categories.Select(x => new CategoryDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle
                }).ToList()

            };

            return Ok(response);


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id)
        {
           var deletedBlogPost = await blogPostRepository.DeleteAsync(id);

            if(deletedBlogPost is null)
            {
                return NotFound();
            }

            var response = new BlogPostDto
            {
                Id = deletedBlogPost.Id,
                Author = deletedBlogPost.Author,
                Content = deletedBlogPost.Content,
                FeatureImageUrl = deletedBlogPost.FeatureImageUrl,
                IsVisible = deletedBlogPost.IsVisible,
                PublishDate = deletedBlogPost.PublishDate,
                ShortDescription = deletedBlogPost.ShortDescription,
                Title = deletedBlogPost.Title,
                UrlHandle = deletedBlogPost.UrlHandle

            };

            return Ok(response);



        }

    }
}
