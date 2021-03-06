﻿using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Managers
{
    public class StaticPageRepoManager
    {
        private IStaticPageRepo repo { get; set; }

        public StaticPageRepoManager(IStaticPageRepo staticPageRepo)
        {
            repo = staticPageRepo;
        }

        public GetStaticPagesResponse GetStaticPages()
        {
            var response = new GetStaticPagesResponse();

            response.Pages = repo.GetStaticPages();

            if (!response.Pages.Any())
            {
                response.Success = false;
                response.Message = "There are no static pages.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public AddStaticPageResponse AddStaticPage(StaticPage page)
        {
            var response = new AddStaticPageResponse();
            var pages = repo.GetStaticPages();

            if(page == null)
            {
                response.Success = false;
                response.Message = $"{page} is not a valid static page.";
            }
            else if(pages.Any(p => p.StaticPageId == page.StaticPageId))
            {
                response.Success = false;
                response.Message = $"{page} already exists.";
            }
            else if(pages.Any(p => p.Title == page.Title))
            {
                response.Success = false;
                response.Message = $"{page.Title} already exists.";
            }
            else
            {
                response.Page = repo.AddStaticPage(page);

                if(response.Page == null)
                {
                    response.Success = false;
                    response.Message = $"{response.Page} is not a valid static page.";
                }
                else
                {
                    response.Success = true;
                }
            }

            return response;
        }

        public GetStaticPageByIdResponse GetPageById(int staticPageId)
        {
            var response = new GetStaticPageByIdResponse();

            response.Page = repo.GetStaticPageById(staticPageId);

            if(response.Page == null)
            {
                response.Success = false;
                response.Message = "No page exists for that id.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public EditStaticPageResponse UpdateStaticPage(StaticPage page)
        {
            var response = new EditStaticPageResponse();

            if(page == null)
            {
                response.Success = false;
                response.Message = $"{page} does not exist.";
            }
            else
            {
                response.Page = repo.UpdateStaticPage(page);

                if(response.Page == null)
                {
                    response.Success = false;
                    response.Message = $"{response.Page} does not exist.";
                }
                else
                {
                    response.Success = true;
                }
            }

            return response;
        }

        public DeleteStaticPageResponse DeleteStaticPage(int staticPageId)
        {
            var response = new DeleteStaticPageResponse();

            response.Success = repo.DeleteStaticPage(staticPageId);

            if (!response.Success)
            {
                response.Message = "Delete was unsuccessful.";
            }

            return response;
        }
    }
}
