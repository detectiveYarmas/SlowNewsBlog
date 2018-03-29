using SlowNewsBlog.Data.Interfaces;
using SlowNewsBlog.Models.Responses;
using SlowNewsBlog.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlowNewsBlog.Domain.Managers
{
    public class HashtagRepoManager
    {
        private IHashTagRepo repo { get; set; }

        public HashtagRepoManager(IHashTagRepo hashTagRepo)
        {
            repo = hashTagRepo;
        }

        public GetHashTagsResponse GetAllHashTags()
        {
            var response = new GetHashTagsResponse();

            response.HashTags = repo.GetAllHashtags();

            if (!response.HashTags.Any())
            {
                response.Success = false;
                response.Message = "There are no hashtags.";
            }
            else
            {
                response.Success = true;
            }

            response.Success = true;
            return response;
        }

        public GetHashTagResponse GetHashTag(int id)
        {
            var response = new GetHashTagResponse();

            response.HashTag = repo.GetHashTag(id);

            if(response.HashTag == null && !response.HashTag.HashTagName.Contains("#"))
            {
                response.Success = false;
                response.Message = "No hashtag associated with this id.";
            }
            else
            {
                response.Success = true;
            }

            
            return response;
        }

        public GetHashTagsResponse GetHashTagsForBlog(int blogPostId)
        {

            var response = new GetHashTagsResponse();
            response.HashTags = repo.GetHashTagsForBlog(blogPostId);
            if (response.HashTags == null)
            {
                response.Success = false;
                response.Message = "No hashtags found for that blog post.";
            }
            else
            {
                response.Success = true;
            }
            return response;
        }

        public ApprovedHashTagsResponse GetApprovedHashtags()
        {
            var response = new ApprovedHashTagsResponse();
            response.HashTags = repo.GetApprovedHashtags();

            if (!response.HashTags.Any())
            {
                response.Success = false;
                response.Message = "There are no approved hashtags.";
                
            }
            else if (response.HashTags.Any(h => h.Approved == false))
            {
                response.Success = false;
                response.Message = "These hashtags should be approved.";
            }
            else
            {
                response.Success = true;
            }

            
            return response;
        }

        public UnapprovedHashTagsResponse GetUnapprovedHashtags()
        {
            var response = new UnapprovedHashTagsResponse();
            response.HashTags = repo.GetUnapprovedHashtags();

            if (!response.HashTags.Any())
            {
                response.Success = false;
                response.Message = "There are no unapproved hashtags.";
                return null;
            }
            else if(response.HashTags.Any(h => h.Approved == true))
            {
                response.Success = false;
                response.Message = "These hashtags should be unapproved.";
            }
            else
            {
                response.Success = true;
            }

            
            return response;
        }

        public AddHashTagResponse AddHashTag(HashTag hashTag)
        {
            var response = new AddHashTagResponse();
            var hashtags = repo.GetAllHashtags();

            if(hashTag == null)
            {
                response.Success = false;
                response.Message = $"{response.Hashtag} is not a valid hashtag.";
            }
            else if(hashtags.Any(h => h.HashTagId == hashTag.HashTagId))
            {
                response.Success = false;
                response.Message = $"{hashTag.HashTagId} already exists.";
            }
            else if(hashtags.Any(h => h.HashTagName == hashTag.HashTagName))
            {
                response.Success = false;
                response.Message = $"{hashTag.HashTagName} already exists.";
            }
            else if (!hashTag.HashTagName.Contains("#"))
            {
                response.Success = false;
                response.Message = $"{hashTag.HashTagName} must begin with a #.";
            }
            else
            {
                response.Hashtag = repo.AddHashTag(hashTag);

                if(response.Hashtag == null)
                {
                    response.Success = false;
                    response.Message = $"{response.Hashtag} does not exist.";
                }
                else
                {
                    response.Success = true;
                }
            }

            return response;
        }

        public DeleteHashTagResponse RemoveHashTag(int id)
        {
            var response = new DeleteHashTagResponse();

            response.Success = repo.RemoveHashTag(id);

            if (!response.Success)
            {
                response.Message = "Delete was unsuccessful.";
            }

            return response;
        }
    }
}
