using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemanHP.Services
{
   public interface ICityDataStore
    {
        Task InitializeProvinceAsync();
        Task InitializeCityAsync();
        Task<List<Province>> GetProvincies();
        Task<List<City>> GetCities(string provinceID);
        Task<CityResult> GetCity(string cityId, string provinceId);
        Task<CostResult> GetCost(string courier, string weight,  string destination);
    }

    public class rajaongkirresult
    {
        public ProvinceResult rajaongkir { get; set; }
    }

    public class rajaongkircity
    {
        public CityResult1 rajaongkir { get; set; }
    }

    public class rajaongkircost
    {
        public CostResult rajaongkir { get; set; }
    }

    public class CityResult1 
    {
        public QueryModel query { get; set; }
        public status status { get; set; }
        public List<City> results { get; set; }
    }


    public class ProvinceResult
    {
        public List<object> query { get; set; }
        public status status { get; set; }
        public  List<Province> results { get; set; }
    }

    public class CityResult
    {
        public QueryModel query { get; set; }
        public status status { get; set; }
        public List<City> results { get; set; }
        public List<City> origin_details { get; set; }
        public List<City> destination_details { get; set; }
    }


    public class Province
    {
        public string province_id { get; set; }
        public string province { get; set; }
    }


    public class City:Province
    {
        public string city_id { get; set; }
        public string type { get; set; }
        public string city_name { get; set; }
        public string postal_code { get; set; }
        public string name { get { return type + " " + city_name; } }
    }

   

    public class QueryModel
    {
        public string province { get; set; }
    }

    public class status
    {
        public int code { get; set; }
        public string description { get; set; }
    }


    public class CostResult
    {
        public QueryModel query { get; set; }
        public status status { get; set; }
        
        public City origin_details { get; set; }
        public City destination_details { get; set; }
        public List<CostResulBase> results { get; set; }
    }

    public class CostResulBase
    {
        public string code { get; set; }
        public string name { get; set; }
      
        public List<CostBase> costs { get; set; }
    }

    public class CostBase
    {
        public string service { get; set; }
        public string description { get; set; }
        public List<cost> cost { get; set; }
    }

    public class cost
    {
        public double value { get; set; }
        public string etd { get; set; }
        public string note { get; set; }
    }


}
