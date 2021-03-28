using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MrMatch.Application.Config.Inp;
using MrMatch.Application.Config.Oup;

namespace MrMatch.Application.Config
{
    public interface IConfigService
    {
        #region country
        /// <summary>
        /// 分页获取国家数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<CountryListOup> GetCountryByPagenation(PagenationInput input);

        /// <summary>
        /// 新增国家
        /// </summary>
        /// <param name="AddOrUpdateCountryInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateCountryAsync(AddOrUpdateCountryInp input);

        /// <summary>
        /// 根据ID获取国家数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<CountryOup> GetCountryByIDAsync(long PKID);

        /// <summary>
        /// 获取所有国家数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        Task<List<AllCountryListOup>> GetAllCountryAsync();
        #endregion

        #region district
        /// <summary>
        /// 分页获取地区数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<DistrictListOup> GetDistrictByPagenation(PagenationInput input);

        /// <summary>
        /// 新增地区
        /// </summary>
        /// <param name="AddOrUpdateDistrictInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateDistrictAsync(AddOrUpdateDistrictInp input);

        /// <summary>
        /// 根据ID获取地区数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<DistrictOup> GetDistrictByIDAsync(long PKID);

        /// <summary>
        /// 获取所有国家数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        Task<List<AllDistrictListOup>> GetAllDistrictAsync();

        /// <summary>
        /// 获取热门城市数据 
        /// </summary>
        /// <returns></returns>
        Task<List<HotDistrictOup>> GetHotDistrictAsync();

        /// <summary>
        /// 获取二级目录城市数据
        /// </summary>
        /// <param name="cache">是否取缓存</param>
        /// <returns></returns>
        Task<List<ProvinceListOup>> GetProvinceListAsync(bool cache);
        #endregion

        #region function
        /// <summary>
        /// 分页获取职能数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<FunctionListOup> GetFunctionByPagenation(PagenationInput input);

        /// <summary>
        /// 新增职能
        /// </summary>
        /// <param name="AddOrUpdateFunctionInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateFunctionAsync(AddOrUpdateFunctionInp input);

        /// <summary>
        /// 新增子职能
        /// </summary>
        /// <param name="parentID"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddChildrenFunctionAsync(AddChildrenFunctionInp input);

        /// <summary>
        /// 根据ID获取职能数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<FunctionOup> GetFunctionByIDAsync(long PKID);

        /// <summary>
        /// 增加关联关系
        /// </summary>
        /// <param name="functionID"></param>
        /// <param name="skillIDs"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddFunctionSkillsAsync(long functionID, string skillIDs);

        /// <summary>
        /// 获取所有职能数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        Task<List<AllFunctionListOup>> GetAllFunctionListAsync();

        /// <summary>
        /// 获取职能技能关联id
        /// </summary>
        /// <param name="functionID"></param>
        /// <returns></returns>
        Task<List<long>> GetFunctionSkillAsync(long functionID);

        /// <summary>
        /// 获取职能三级目录
        /// </summary>
        /// <param name="cache">是否取缓存</param>
        /// <returns></returns>
        Task<List<FunctionFirstCascaderOup>> GetFunctionForCascaderAsync(bool cache);
        #endregion

        #region skills
        /// <summary>
        /// 分页获取技能数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<SkillsListOup> GetSkillsByPagenation(PagenationInput input);

        /// <summary>
        /// 新增技能
        /// </summary>
        /// <param name="AddOrUpdateSkillsInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateSkillsAsync(AddOrUpdateSkillsInp input);

        /// <summary>
        /// 根据ID获取技能数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<SkillsOup> GetSkillsByIDAsync(long PKID);

        /// <summary>
        /// 获取所有技能数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        Task<List<AllSkillListOup>> GetAllSkillsListAsync();
        #endregion

        #region tags
        /// <summary>
        /// 分页获取标签数据
        /// </summary>
        /// <param name="pagenationInput"></param>
        /// <returns></returns>
        PagenationOutput<TagsListOup> GetTagsByPagenation(PagenationInput input);

        /// <summary>
        /// 新增标签
        /// </summary>
        /// <param name="AddOrUpdateTagsInp"></param>
        /// <returns></returns>
        Task<BoolMessageOup> AddOrUpdateTagsAsync(AddOrUpdateTagsInp input);

        /// <summary>
        /// 根据ID获取标签数据
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        Task<TagsOup> GetTagsByIDAsync(long PKID);

        /// <summary>
        /// 获取所有标签数据(PKID+Name)
        /// </summary>
        /// <returns></returns>
        Task<List<AllTagsListOup>> GetAllTagsListAsync();

        /// <summary>
        /// 获取所有父标签数据
        /// </summary>
        /// <returns></returns>
        Task<List<AllParentTagsListOup>> GetAllParentTagsListAsync();

        /// <summary>
        /// 获取分层的标签数据
        /// </summary>
        /// <returns></returns>
        Task<List<FirtstTagListOup>> GetTagsListAsync();
        #endregion
    }
}
