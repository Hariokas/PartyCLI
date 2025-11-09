namespace partycli
{
    internal class VpnServerQuery
    {
        public VpnServerQuery(int? protocol, int? countryId, int? cityId, int? regionId, int? specificServerId,
            int? serverGroupId)
        {
            Protocol = protocol;
            CountryId = countryId;
            CityId = cityId;
            RegionId = regionId;
            SpecificServerId = specificServerId;
            ServerGroupId = serverGroupId;
        }

        public int? Protocol { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }

        public int? RegionId { get; set; }

        public int? SpecificServerId { get; set; }

        public int? ServerGroupId { get; set; }
    }
}