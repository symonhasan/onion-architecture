using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OA.Data;
using OA.Service;
using OA.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OA.Web.Controllers
{
    [Authorize]
    public class PlaceController : Controller
    {
        private readonly ITouristPlaceService touristPlaceService;
        private readonly ITouristPlaceTypeService touristPlaceTypeService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;

        public PlaceController(IMapper mapper , ITouristPlaceService touristPlaceService, ITouristPlaceTypeService touristPlaceTypeService , IWebHostEnvironment hostEnvironment)
        {
            this.touristPlaceService = touristPlaceService;
            this.touristPlaceTypeService = touristPlaceTypeService;
            _hostEnvironment = hostEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index(string search, string sort)
        {
            dynamic touristPlaces = touristPlaceService.GetTouristPlaces();

            List<TouristPlaceViewModel> places = new List<TouristPlaceViewModel>();

            foreach (dynamic place in touristPlaces)
            {
                TouristPlaceViewModel p = _mapper.Map<TouristPlaceViewModel>(place);
                p.TypeName = touristPlaceTypeService.GetTouristPlaceType(place.TypeId).TypeName;
                places.Add(p);
            }

            TempData["search"] = search;
            TempData["sort"] = sort;

            if (search == null)
            {
                if (sort != null && sort == "asc")
                {
                    var sorted = places.OrderBy(place => place.Rating);
                    return View(sorted);
                }
                else if (sort != null && sort == "desc")
                {
                    var sorted = places.OrderByDescending(place => place.Rating);
                    return View(sorted);
                }
                return View(places);
            }
            else
            {
                var filtered = places.Where(place => place.Name.ToLower().Contains(search.ToLower()));
                if (sort != null && sort == "asc")
                {
                    var sorted = filtered.OrderBy(place => place.Rating);
                    return View(sorted);
                }
                else if (sort != null && sort == "desc")
                {
                    var sorted = filtered.OrderByDescending(place => place.Rating);
                    return View(sorted);
                }
                return View(filtered.ToList());
            }
        }

        public IActionResult Create()
        {
            var place = new TouristPlaceViewModel
            {
                Rating = 1
            };
            ViewBag.Types = new SelectList(touristPlaceTypeService.GetTouristPlaceTypes(), "Id", "TypeName");
            return View(place);
        }

        [HttpPost , ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,TypeId,Rating,ImageFile")] TouristPlaceViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    model.Image = fileName = fileName + Guid.NewGuid() + extension;
                    string path = Path.Combine(wwwRootPath + "/image/", fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    model.ImageFile.CopyTo(fileStream);
                }
                TouristPlace entity = _mapper.Map<TouristPlace>(model);
                touristPlaceService.InsertTouristPlace(entity);
                if (entity.Id > 0)
                {
                    return Redirect(GetRedirectUrl());
                }
            }
            ViewBag.Types = new SelectList(touristPlaceTypeService.GetTouristPlaceTypes(), "Id", "TypeName");
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var place = touristPlaceService.GetTouristPlace(id);
            if( place == null)
            {
                return NotFound();
            }
            TouristPlaceViewModel model = _mapper.Map<TouristPlaceViewModel>(place);
            model.TypeName = touristPlaceTypeService.GetTouristPlaceType(model.TypeId).TypeName;
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var place = touristPlaceService.GetTouristPlace(id);
            if( place == null)
            {
                return NotFound();
            }
            ViewBag.Types = new SelectList(touristPlaceTypeService.GetTouristPlaceTypes(), "Id", "TypeName");
            TouristPlaceViewModel model = _mapper.Map<TouristPlaceViewModel>(place);
            model.TypeName = touristPlaceTypeService.GetTouristPlaceType(model.TypeId).TypeName;
            return View(model);
        }

        [HttpPost , ValidateAntiForgeryToken]
        public IActionResult Edit(int id , [Bind("Id,Name,Address,Rating,TypeId,ImageFile,Image")] TouristPlaceViewModel model)
        {
            dynamic place = touristPlaceService.GetTouristPlace(id);
            if (place.Id != model.Id)
            {
                return NotFound();
            }
            if( ModelState.IsValid)
            {
                if (model.ImageFile != null)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    if (model.Image != null)
                    {
                        string placePrevImgLocation = Path.Combine(wwwRootPath, "image", model.Image);
                        if (System.IO.File.Exists(placePrevImgLocation))
                        {
                            System.IO.File.Delete(placePrevImgLocation);
                        }
                    }
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    model.Image = fileName = fileName + Guid.NewGuid() + extension;
                    string path = Path.Combine(wwwRootPath + "/image/", fileName);
                    using var fileStream = new FileStream(path, FileMode.Create);
                    model.ImageFile.CopyTo(fileStream);
                }
                place.Id = model.Id;
                place.Name = model.Name;
                place.Address = model.Address;
                place.TypeId = model.TypeId;
                place.Rating = model.Rating;
                place.Image = model.Image;

                touristPlaceService.UpdateTouristPlace(place);
                return Redirect(GetRedirectUrl());
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            dynamic place = touristPlaceService.GetTouristPlace(id);
            if( place == null )
            {
                return NotFound();
            }
            TouristPlaceViewModel model = _mapper.Map<TouristPlaceViewModel>(place);
            model.TypeName = touristPlaceTypeService.GetTouristPlaceType(model.TypeId).TypeName;
            return View(model);
        }

        [HttpPost , ActionName("Delete") , ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            dynamic place = touristPlaceService.GetTouristPlace(id);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            if (place.Image != null)
            {
                string placePrevImgLocation = Path.Combine(wwwRootPath, "image", place.Image);
                if (System.IO.File.Exists(placePrevImgLocation))
                {
                    System.IO.File.Delete(placePrevImgLocation);
                }
            }
            touristPlaceService.DeleteTouristPlace(place);
            return Redirect(GetRedirectUrl());
        }

        private string GetRedirectUrl()
        {
            string redirectUrl = "/Place";
            if (TempData["search"] != null)
            {
                redirectUrl += "?search=" + TempData["search"].ToString();
            }
            if (TempData["search"] == null && TempData["sort"] != null)
            {
                redirectUrl += "?sort=" + TempData["sort"].ToString();
            }
            if (TempData["search"] != null && TempData["sort"] != null)
            {
                redirectUrl += "&sort=" + TempData["sort"].ToString();
            }
            return redirectUrl;
        }

        public dynamic CheckRedirection()
        {
            if (TempData.ContainsKey("search") && TempData["search"] != null)
            {
                return Redirect(GetRedirectUrl());
            }
            else if (TempData.ContainsKey("sort") && TempData["sort"] != null)
            {
                return Redirect(GetRedirectUrl());
            }
            return Redirect("/Place");
        }
    }
}
