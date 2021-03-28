using MrMatch.Application.Config.Inp;
using MrMatch.Application.Config.Oup;
using MrMatch.Domain.EntityBase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Domain.Models;
using MrMatch.Common.Mapper;
using MrMatch.MysqlFramework.Extensions;

namespace MrMatch.Application.Config
{
    public class ConfigService : IConfigService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<TP_Country> country;
        private readonly IRepository<TP_District> district;
        private readonly IRepository<TP_Function> function;
        private readonly IRepository<TP_Skills> skills;
        private readonly IRepository<TP_Tags> tags;
        private readonly IRepository<TP_FunctionSkillsRelation> relation;
        public ConfigService(
            IUnitOfWork _unitOfWork,
            IRepository<TP_Country> _country,
            IRepository<TP_District> _district,
            IRepository<TP_Function> _function,
            IRepository<TP_Skills> _skills,
            IRepository<TP_Tags> _tags,
            IRepository<TP_FunctionSkillsRelation> _relation)
        {
            unitOfWork = _unitOfWork;
            country = _country;
            district = _district;
            function = _function;
            skills = _skills;
            tags = _tags;
            relation = _relation;
        }


        #region country
        public PagenationOutput<CountryListOup> GetCountryByPagenation(PagenationInput input)
        {
            PagenationOutput<CountryListOup> data = new PagenationOutput<CountryListOup>();
            var total = 0;
            var pageCount = 0;
            var query = country.GetAll(x => x.Valid == true).WhereIf(x => x.CountryName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = country.GetByPagenation<int>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.OrderNum,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Country, CountryListOup>();
            return data;
        }
        public async Task<BoolMessageOup> AddOrUpdateCountryAsync(AddOrUpdateCountryInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {
                var addData = input.MapTo<AddOrUpdateCountryInp, TP_Country>();
                addData.CreatedTime = now;
                addData.LastModifiedTime = now;
                addData.Valid = true;
                unitOfWork.RegisterNew(addData);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateData = await country.GetAsync(input.PKID);
                if (!updateData.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateData = input.MapTo<TP_Country>(updateData);
                updateData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }
        public async Task<CountryOup> GetCountryByIDAsync(long PKID)
        {
            var countryData = await country.GetAsync(PKID);
            if (!countryData.IsNullEntity())
            {
                return null;
            }

            var oup = countryData.MapTo<TP_Country, CountryOup>();
            return oup;
        }
        public async Task<List<AllCountryListOup>> GetAllCountryAsync()
        {
            var list = await country.GetAllListAsync(x => x.Valid == true);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_Country, AllCountryListOup>();
            return oup;
        }

        #endregion

        #region district
        public PagenationOutput<DistrictListOup> GetDistrictByPagenation(PagenationInput input)
        {
            PagenationOutput<DistrictListOup> data = new PagenationOutput<DistrictListOup>();
            var total = 0;
            var pageCount = 0;
            var query = district.GetAll(x => x.Valid == true).WhereIf(x => x.DistrictName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = district.GetByPagenation<int>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.OrderNum,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_District, DistrictListOup>();
            var ids = data.DataList.Select(x => x.ParentID).ToList();
            var contryids = data.DataList.Select(x => x.CountryID).ToList();
            if (ids.Count > 0)
            {
                var allDistrict = district.GetAll(x => ids.Contains(x.PKID)).ToList();
                data.DataList.ForEach(item =>
                {
                    if (item.ParentID == 0)
                    {
                        item.ParentName = "无";
                    }
                    else
                    {
                        item.ParentName = allDistrict.Find(x => x.PKID == item.ParentID).DistrictName;
                    }
                });
            }
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateDistrictAsync(AddOrUpdateDistrictInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;


            //新增
            if (input.PKID <= 0)
            {
                //排重
                var existData = await district.FirstOrDefaultAsync(x => x.DistrictName == input.DistrictName && x.Valid == true);

                if (existData.IsNullEntity())
                {
                    rtn.Message = "此地区已存在,请更换";
                    rtn.BoolResult = false;
                    return rtn;
                }

                var addData = input.MapTo<AddOrUpdateDistrictInp, TP_District>();
                addData.CreatedTime = now;
                addData.LastModifiedTime = now;
                addData.Valid = true;
                unitOfWork.RegisterNew(addData);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateData = await district.GetAsync(input.PKID);
                if (!updateData.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }


                updateData = input.MapTo<TP_District>(updateData);
                updateData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<DistrictOup> GetDistrictByIDAsync(long PKID)
        {
            var districtData = await district.GetAsync(PKID);
            if (!districtData.IsNullEntity())
            {
                return null;
            }

            var oup = districtData.MapTo<TP_District, DistrictOup>();
            return oup;
        }
        public async Task<List<AllDistrictListOup>> GetAllDistrictAsync()
        {
            var list = await district.GetAllListAsync(x => x.Valid == true);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_District, AllDistrictListOup>();
            return oup;
        }

        public async Task<List<HotDistrictOup>> GetHotDistrictAsync()
        {
            var list = await district.GetAllListAsync(x => x.Valid == true && x.HotCity == true);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_District, HotDistrictOup>();

            return oup;
        }

        public async Task<List<ProvinceListOup>> GetProvinceListAsync(bool cache)
        {
            var oup = new List<ProvinceListOup>();
            if (cache)
            {
                oup = Cache.GetCacheHelper.GetBasicCity();
                if (!oup.IsNullEntity())
                {
                    var list = await district.GetAllListAsync(x => x.Valid == true && x.ParentID == 0);
                    if (list.Count <= 0)
                    {
                        return null;
                    }
                    var parentids = list.Select(x => x.PKID).ToList();

                    var city = await district.GetAllListAsync(x => parentids.Contains(x.ParentID));
                    if (city.Count <= 0)
                    {
                        return null;
                    }
                    oup = LogicHelper.ProvinceList(list, city);
                }

            }
            else
            {
                var list = await district.GetAllListAsync(x => x.Valid == true && x.ParentID == 0);
                if (list.Count <= 0)
                {
                    return null;
                }
                var parentids = list.Select(x => x.PKID).ToList();

                var city = await district.GetAllListAsync(x => parentids.Contains(x.ParentID) && x.Valid == true);
                if (city.Count <= 0)
                {
                    return null;
                }
                oup = LogicHelper.ProvinceList(list, city);
            }


            return oup;
        }

        #endregion

        #region function
        public PagenationOutput<FunctionListOup> GetFunctionByPagenation(PagenationInput input)
        {
            PagenationOutput<FunctionListOup> data = new PagenationOutput<FunctionListOup>();
            var total = 0;
            var pageCount = 0;
            var query = function.GetAll(x => x.Valid == true).WhereIf(x => x.FunctionName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = function.GetByPagenation<int>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.SortNum,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Function, FunctionListOup>();
            var ids = data.DataList.Select(x => x.ParentID).ToList();
            if (ids.Count > 0)
            {
                var allFunction = function.GetAll(x => ids.Contains(x.PKID)).ToList();
                data.DataList.ForEach(item =>
                {
                    if (item.ParentID == 0)
                    {
                        item.ParentName = "无";
                    }
                    else
                    {
                        item.ParentName = allFunction.Find(x => x.PKID == item.ParentID).FunctionName;
                    }
                });
            }
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateFunctionAsync(AddOrUpdateFunctionInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;


            //新增
            if (input.PKID <= 0)
            {
                //排重
                var existData = await function.FirstOrDefaultAsync(x => x.FunctionName == input.FunctionName && x.ParentID == input.ParentID && x.Valid == true);

                if (existData.IsNullEntity())
                {
                    rtn.Message = "此职能已存在,请更换";
                    rtn.BoolResult = false;
                    return rtn;
                }

                var addData = input.MapTo<AddOrUpdateFunctionInp, TP_Function>();
                addData.CreatedTime = now;
                addData.LastModifiedTime = now;
                addData.Valid = true;
                unitOfWork.RegisterNew(addData);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateData = await function.GetAsync(input.PKID);
                if (!updateData.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }


                //排重
                var existData = await function.FirstOrDefaultAsync(x => x.FunctionName == input.FunctionName && x.ParentID == input.ParentID && x.Valid == true);

                if (existData.IsNullEntity())
                {
                    rtn.Message = "此职能已存在,请更换";
                    rtn.BoolResult = false;
                    return rtn;
                }


                updateData = input.MapTo<TP_Function>(updateData);
                updateData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<BoolMessageOup> AddChildrenFunctionAsync(AddChildrenFunctionInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //排重
            var existData = await function.FirstOrDefaultAsync(x => x.FunctionName == input.FunctionName && x.ParentID == input.ParentID && x.Valid == true);

            if (existData.IsNullEntity())
            {
                rtn.Message = "此职能已存在,请更换";
                rtn.BoolResult = false;
                return rtn;
            }

            var addData = input.MapTo<AddChildrenFunctionInp, TP_Function>();
            addData.IsPopular = false;
            addData.CreatedTime = now;
            addData.LastModifiedTime = now;
            addData.Valid = true;
            unitOfWork.RegisterNew(addData);

            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "新增成功";
            }
            else
            {
                rtn.Message = "新增失败";
            }
            return rtn;
        }

        public async Task<FunctionOup> GetFunctionByIDAsync(long PKID)
        {
            var functionData = await function.GetAsync(PKID);
            if (!functionData.IsNullEntity())
            {
                return null;
            }

            var oup = functionData.MapTo<TP_Function, FunctionOup>();
            return oup;
        }

        public async Task<BoolMessageOup> AddFunctionSkillsAsync(long functionID, string skillIDs)
        {
            var rtn = new BoolMessageOup();

            //排重
            var existData = await function.FirstOrDefaultAsync(x => x.PKID == functionID);

            if (!existData.IsNullEntity())
            {
                rtn.Message = "未查到目标数据";
                rtn.BoolResult = false;
                return rtn;
            }

            var now = DateTime.Now;

            var ids = skillIDs.Split(',').ToList();

            ids.ForEach(async item =>
            {
                var id = Convert.ToInt32(item);
                var existRlation = await relation.FirstOrDefaultAsync(x => x.FunctionID == functionID && x.SkillID == id);
                if (!existRlation.IsNullEntity())
                {
                    TP_FunctionSkillsRelation functionRelation = new TP_FunctionSkillsRelation();
                    functionRelation.CreatedTime = now;
                    functionRelation.LastModifiedTime = now;
                    functionRelation.FunctionID = functionID;
                    functionRelation.SkillID = id;
                    functionRelation.Valid = true;
                    unitOfWork.RegisterNew(functionRelation);
                }
            });

            rtn.BoolResult = await unitOfWork.CommitAsync();
            if (rtn.BoolResult)
            {
                rtn.Message = "新增成功";
            }
            else
            {
                rtn.Message = "新增失败";
            }
            return rtn;
        }

        public async Task<List<AllFunctionListOup>> GetAllFunctionListAsync()
        {
            var list = await function.GetAllListAsync(x => x.Valid == true);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_Function, AllFunctionListOup>();
            return oup;
        }

        public async Task<List<long>> GetFunctionSkillAsync(long functionID)
        {
            var relations = await relation.GetAllListAsync(x => x.FunctionID == functionID);
            return relations.Select(x => x.SkillID).ToList();
        }

        public async Task<List<FunctionFirstCascaderOup>> GetFunctionForCascaderAsync(bool cache)
        {
            var oup = new List<FunctionFirstCascaderOup>();
            if (cache)
            {
                oup = Cache.GetCacheHelper.GetFunctionForCascader();
                if (!oup.IsNullEntity())
                {
                    var allFunc = (await function.GetAllListAsync(x => x.Valid == true && x.OnEnabled == true))
                    .OrderByDescending(x => x.SortNum).ToList();

                    var firstFunc = allFunc.Where(x => x.ParentID == 0).OrderByDescending(x => x.SortNum).ToList();
                    var firstIds = firstFunc.Select(x => x.PKID).ToList();

                    var secondFunc = allFunc.Where(x => firstIds.Contains(x.ParentID)).OrderByDescending(x => x.SortNum).ToList();
                    var secondids = secondFunc.Select(x => x.PKID).ToList();

                    var thirdFunc = allFunc.Where(x => secondids.Contains(x.ParentID)).OrderByDescending(x => x.SortNum).ToList();
                    oup = LogicHelper.Cascader(firstFunc, secondFunc, thirdFunc);
                }
            }
            else
            {
                var allFunc = (await function.GetAllListAsync(x => x.Valid == true && x.OnEnabled == true))
                .OrderByDescending(x => x.SortNum).ToList();

                var firstFunc = allFunc.Where(x => x.ParentID == 0).OrderByDescending(x => x.SortNum).ToList();
                var firstIds = firstFunc.Select(x => x.PKID).ToList();

                var secondFunc = allFunc.Where(x => firstIds.Contains(x.ParentID)).OrderByDescending(x => x.SortNum).ToList();
                var secondids = secondFunc.Select(x => x.PKID).ToList();

                var thirdFunc = allFunc.Where(x => secondids.Contains(x.ParentID)).OrderByDescending(x => x.SortNum).ToList();
                oup = LogicHelper.Cascader(firstFunc, secondFunc, thirdFunc);
            }


            return oup;
        }
        #endregion

        #region skills
        public PagenationOutput<SkillsListOup> GetSkillsByPagenation(PagenationInput input)
        {
            PagenationOutput<SkillsListOup> data = new PagenationOutput<SkillsListOup>();
            var total = 0;
            var pageCount = 0;
            var query = skills.GetAll(x => x.Valid == true).WhereIf(x => x.SkillName.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = skills.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Skills, SkillsListOup>();
            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateSkillsAsync(AddOrUpdateSkillsInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {

                //排重
                var existData = await skills.FirstOrDefaultAsync(x => x.SkillName == input.SkillName && x.Valid == true);

                if (existData.IsNullEntity())
                {
                    rtn.Message = "此技能已存在,请更换";
                    rtn.BoolResult = false;
                    return rtn;
                }
                var addData = input.MapTo<AddOrUpdateSkillsInp, TP_Skills>();
                addData.CreatedTime = now;
                addData.LastModifiedTime = now;
                addData.Valid = true;
                unitOfWork.RegisterNew(addData);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateData = await skills.GetAsync(input.PKID);
                if (!updateData.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateData = input.MapTo<TP_Skills>(updateData);
                updateData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<SkillsOup> GetSkillsByIDAsync(long PKID)
        {
            var skillsData = await skills.GetAsync(PKID);
            if (!skillsData.IsNullEntity())
            {
                return null;
            }

            var oup = skillsData.MapTo<TP_Skills, SkillsOup>();
            return oup;
        }
        public async Task<List<AllSkillListOup>> GetAllSkillsListAsync()
        {
            var list = await skills.GetAllListAsync(x => x.Valid == true);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_Skills, AllSkillListOup>();
            return oup;
        }
        #endregion

        #region tags
        public PagenationOutput<TagsListOup> GetTagsByPagenation(PagenationInput input)
        {
            PagenationOutput<TagsListOup> data = new PagenationOutput<TagsListOup>();
            var total = 0;
            var pageCount = 0;
            var query = tags.GetAll(x => x.Valid == true).WhereIf(x => x.Tags.Contains(input.KeyWords), !string.IsNullOrEmpty(input.KeyWords));
            var list = tags.GetByPagenation<long>
                (input.PageIndex,
                input.PageSize,
                out total,
                out pageCount,
                query,
                x => x.PKID,
                input.IsAsc);
            data.PageIndex = input.PageIndex;
            data.PageCount = pageCount;
            data.Total = total;
            data.DataList = list.MapToList<TP_Tags, TagsListOup>();
            var ids = data.DataList.Select(x => x.ParentID).ToList();
            if (ids.Count > 0)
            {
                var allTags = tags.GetAll(x => ids.Contains(x.PKID)).ToList();
                data.DataList.ForEach(item =>
                {
                    if (item.ParentID == 0)
                    {
                        item.ParentTag = "无";
                    }
                    else
                    {
                        item.ParentTag = allTags.Find(x => x.PKID == item.ParentID).Tags;
                    }
                });
            }

            return data;
        }

        public async Task<BoolMessageOup> AddOrUpdateTagsAsync(AddOrUpdateTagsInp input)
        {
            var rtn = new BoolMessageOup();

            var now = DateTime.Now;

            //新增
            if (input.PKID <= 0)
            {

                //排重
                var existData = await tags.FirstOrDefaultAsync(x => x.Tags == input.Tags && x.Valid == true);

                if (existData.IsNullEntity())
                {
                    rtn.Message = "此标签已存在,请更换";
                    rtn.BoolResult = false;
                    return rtn;
                }
                var addData = input.MapTo<AddOrUpdateTagsInp, TP_Tags>();
                addData.CreatedTime = now;
                addData.LastModifiedTime = now;
                addData.Valid = true;
                unitOfWork.RegisterNew(addData);

                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "新增成功";
                }
                else
                {
                    rtn.Message = "新增失败";
                }
            }
            else
            {
                var updateData = await tags.GetAsync(input.PKID);
                if (!updateData.IsNullEntity())
                {
                    rtn.BoolResult = false;
                    rtn.Message = "找不到目标数据";
                    return rtn;
                }

                updateData = input.MapTo<TP_Tags>(updateData);
                updateData.LastModifiedTime = now;
                unitOfWork.RegisterUpdate(updateData);
                rtn.BoolResult = await unitOfWork.CommitAsync();
                if (rtn.BoolResult)
                {
                    rtn.Message = "修改成功";
                }
                else
                {
                    rtn.Message = "修改失败";
                }
            }
            return rtn;
        }

        public async Task<TagsOup> GetTagsByIDAsync(long PKID)
        {
            var tagsData = await tags.GetAsync(PKID);
            if (!tagsData.IsNullEntity())
            {
                return null;
            }

            var oup = tagsData.MapTo<TP_Tags, TagsOup>();
            return oup;
        }

        public async Task<List<AllTagsListOup>> GetAllTagsListAsync()
        {
            var list = await tags.GetAllListAsync(x => x.Valid == true);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_Tags, AllTagsListOup>();
            return oup;
        }

        public async Task<List<AllParentTagsListOup>> GetAllParentTagsListAsync()
        {
            var list = await tags.GetAllListAsync(x => x.Valid == true && x.ParentID == 0);
            if (list.Count <= 0)
            {
                return null;
            }
            var oup = list.MapToList<TP_Tags, AllParentTagsListOup>();
            return oup;
        }

        public async Task<List<FirtstTagListOup>> GetTagsListAsync()
        {
            var firstTags = (await tags.GetAllListAsync(x => x.Valid == true && x.ParentID == 0))
                .MapToList<TP_Tags, FirtstTagListOup>();
            var secondTags = (await tags.GetAllListAsync(x => x.Valid == true && x.ParentID != 0))
                .MapToList<TP_Tags, TagListOup>();
            firstTags.ForEach(i =>
            {
                var data = secondTags.FindAll(x => x.ParentID == i.PKID);
                i.ChildrenTag = data;
            });
            return firstTags;
        }
        #endregion
    }
}
