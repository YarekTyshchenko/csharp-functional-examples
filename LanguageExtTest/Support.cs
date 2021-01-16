// <copyright file="Support.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest
{
    using System;
    using System.Threading.Tasks;
    using LanguageExt;
    using LanguageExt.Common;

    public record Device(Guid GatewayId);
    public record Gateway(string ConfigurationToken);
    public record DeviceConfiguration();

    public class DeviceConfigurationReader
    {
        public Task<DeviceConfiguration> GetLatestDeviceConfigurationByDeviceAsync(string o, Guid d) =>
            throw new NotImplementedException();
        public EitherAsync<Error, DeviceConfiguration> GetLatestDeviceConfigurationByDevice(string o, Guid d) =>
            // $"DeviceId {deviceId} Not found"
            throw new NotImplementedException();
    }

    public class GatewayReader
    {
        public Task<Gateway> GetGatewayAsync(string o, Guid g) =>
            throw new NotImplementedException();
        public OptionAsync<Gateway> GetGateway(string i, Guid o) =>
            throw new NotImplementedException();
    }

    public class DeviceReader
    {
        public Task<Device> GetDeviceAsync(string a, Guid b) =>
            throw new NotImplementedException();
        public OptionAsync<Device> GetDevice(string a, Guid b) =>
            throw new NotImplementedException();
    }

}
