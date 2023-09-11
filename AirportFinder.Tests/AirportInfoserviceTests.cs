using Airportfinder.Models;
using Airportfinder.RepositoryPattern;
using Airportfinder.Services;
using Airportfinder.Services.Implementation;
using Moq;

namespace AirportFinderTests
{
    public class AirportnfoserviceTests
    {
        private readonly Mock<IRepository<AirportInfo>> _airportRepository;
        private readonly Mock<ICityInfo> _cityinfoService;

        public AirportnfoserviceTests()
        {
            _airportRepository = new Mock<IRepository<AirportInfo>>();
            _cityinfoService = new Mock<ICityInfo>();
        }

        [Fact]
        public void GetAirportList_Should_return_Success()
        {
            //arrange
            var Id = "TamilNadu";
            _airportRepository.Setup(x => x.Get()).Returns(GetAirportsList());
            _cityinfoService.Setup(x => x.GetCityList()).Returns(GetCityInfoList());

            //act
            AirportInfoService info = new AirportInfoService(_airportRepository.Object, _cityinfoService.Object);
            var result = info.GetAirportsbyId(Id);

            //assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAirportsList_Should_returnZeroResult()
        {
            //Arrange
            var Id = "Gujarat";
            _airportRepository.Setup(x => x.Get()).Returns(GetAirportsList());

            //Act&Assertions
            AirportInfoService info = new AirportInfoService(_airportRepository.Object, _cityinfoService.Object);
            var result = info.GetAirportsbyId(Id);

            Assert.False(result.Count > 0);
        }

        [Fact]
        public void FindingCost_Should_Success()
        {
            //Arrange
            string from = "Chennai International Airport";
            string to = "Kempegowda International Airport";
            const double dist = 267.7506;
            const double cost = 3893.0939;
            _airportRepository.Setup(x => x.Get()).Returns(GetAirportInfoList());
            _cityinfoService.Setup(x => x.GetCityList()).Returns(GetCityInfoList());

            //Act 
            AirportInfoService info = new AirportInfoService(_airportRepository.Object, _cityinfoService.Object);
            Tuple<string, string> result = info.GetCostDetails(from, to);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(dist, Convert.ToDouble(result.Item1));
            Assert.Equal(cost, Convert.ToDouble(result.Item2));
        }

        [Fact]
        public void GetCostDetails_Should_ThrowException()
        {
            //Arrange
            string from = "Chennai International Airport";
            string to = " Tiruchirappalli International Airport";
            const double dist = 267.7506;
            const double cost = 3893.0939;
            _airportRepository.Setup(x => x.Get()).Returns(GetAirportInfoList());
            _cityinfoService.Setup(x => x.GetCityList()).Returns(GetCityInfoList());

            //Act&Assertions
            AirportInfoService info = new AirportInfoService(_airportRepository.Object, _cityinfoService.Object);
            Assert.Throws<NullReferenceException>(() => info.GetCostDetails(from, to));

        }

        [Theory]
        [InlineData("Chennai", "Salem")]
        public void FindingDistance_between_two_Airports_Should_be_success(string From, string To)
        {
            ////Arrange
            _airportRepository.Setup(x => x.Get()).Returns(GetAirportInfoList());
            _cityinfoService.Setup(x => x.GetCityList()).Returns(GetCityInfoList());

            //Act
            AirportInfoService info = new AirportInfoService(_airportRepository.Object, _cityinfoService.Object);
            var result = info.GetAirportsandDistance(From, To);

            //Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 0);
        }

        [Fact]
        public void FindingDistance_between_two_Airports_Should_throwException()
        {
            ////Arrange
            string From = "Hyderabad";
            string To = "Salem";
            _airportRepository.Setup(x => x.Get()).Returns(GetAirportInfoList());
            _cityinfoService.Setup(x => x.GetCityList()).Returns(GetCityInfoList());

            //Act & Assertions
            AirportInfoService info = new AirportInfoService(_airportRepository.Object, _cityinfoService.Object);
            Assert.Throws<NullReferenceException>(() => info.GetAirportsandDistance(From, To));

        }

        private List<AirportInfo> GetAirportInfoList()
        {
            return new List<AirportInfo> {
             new AirportInfo(){ AirportName="Chennai International Airport", City="Chennai",Country="India",IataCode="MAA",Latitude=12.990005,Longitude=80.169296,State="Tamil Nadu"},
             new AirportInfo(){ AirportName="Vellore Airport", City="Vellore",Country="India",IataCode="VLE",Latitude=12.90880013,Longitude=79.06680298,State="Tamil Nadu"},
             new AirportInfo(){ AirportName="Pondicherry Airport", City="Puducherry",Country="India",IataCode="PLY",Latitude=11.968,Longitude=79.812,State="Pondicherry"},
             new AirportInfo(){ AirportName="Salem Airport", City="Salem",Country="India",IataCode="SXE",Latitude=11.7833004,Longitude=78.06559753,State="Tamil Nadu"},
             new AirportInfo(){AirportName="Kempegowda International Airport",City="Bangalore",Country="India",IataCode="BLR",Latitude=13.1979,Longitude=77.706299}

            };
        }

        private List<AirportInfo> GetAirportsList()
        {
            return new List<AirportInfo>
            {
                new AirportInfo(){ AirportName="Chennai International Airport", City="Chennai",IataCode="MAA"},
                new AirportInfo(){ AirportName="Coimbatore International Airport", City="Coimbatore",IataCode="CJB"},
                new AirportInfo(){AirportName="Madurai Airport",City="Madurai",IataCode="IXM"},
                new AirportInfo(){AirportName="Kovilpatti Airport",City="Nallatinputhur",IataCode="KPI"},
                new AirportInfo(){AirportName="Neyveli Airport",City="Neyveli",IataCode="NVY"},
                new AirportInfo(){AirportName="Tiruchirappalli International Airport",City="Tiruchirappalli",IataCode="TRZ"}

            };
        }

        private List<CityInfo> GetCityInfoList()
        {
            return new List<CityInfo> {
            new CityInfo(){ CityName="Chennai",Latitude=13.0836939,Longitude=80.270186 },
            new CityInfo(){ CityName="Salem",Latitude=11.65212,Longitude=78.157982 }
            };
        }
    }
}