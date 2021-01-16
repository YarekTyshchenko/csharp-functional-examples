// <copyright file="Class1.cs" company="HARK">
// Copyright (c) HARK. All rights reserved.
// </copyright>

namespace LanguageExtTest
{
    using System;
    using System.Threading.Tasks;
    using LanguageExt;
    using static LanguageExt.Prelude;

    public class Class1
    {
        private DeviceReader DeviceReader { get; }
        private GatewayReader GatewayReader { get; }
        private DeviceConfigurationReader DeviceConfigurationReader { get; }

        public Class1(
            DeviceReader deviceReader,
            GatewayReader gatewayReader,
            DeviceConfigurationReader deviceConfigurationReader)
        {
            DeviceReader = deviceReader;
            GatewayReader = gatewayReader;
            DeviceConfigurationReader = deviceConfigurationReader;
        }

        /**
         * Imperative style describes what happens at every step.
         */
        public async Task<Either<string, DeviceConfiguration>> Imperative(string organizationId, string token, Guid deviceId)
        {
            var device = await this.DeviceReader.GetDeviceAsync(organizationId, deviceId);
            if (device is null)
            {
                return "Invalid token";
            }

            var gateway = await this.GatewayReader.GetGatewayAsync(organizationId, device.GatewayId);

            // Prevent enumeration attacks by not leaking valid device IDs
            if (gateway is null || gateway.ConfigurationToken != token)
            {
                return "Invalid Token";
            }

            var config = await this.DeviceConfigurationReader.GetLatestDeviceConfigurationByDeviceAsync(organizationId, deviceId);
            if (config is null)
            {
                return $"DeviceId {deviceId} Not found";
            }

            return config;
        }

        /**
         * Wrap Task results in Option and Either. Compose them with Bind, which is like a Map function that by
         * convention favours the righty value.
         */
        public async Task<Either<string, DeviceConfiguration>> F1(string organizationId, string token, Guid deviceId)
        {
            var device = Optional(await this.DeviceReader.GetDeviceAsync(organizationId, deviceId))
                .ToEither("Invalid Token");

            var f2 = await device.BindAsync(async d =>
                Optional(await this.GatewayReader.GetGatewayAsync(organizationId, d.GatewayId))
                    .ToEither("Invalid Token"));

            var f3 = f2.Bind<Gateway>(g => g.ConfigurationToken != token ? g : "Invalid Token");

            var config = await f3.BindAsync(async g =>
                Optional(await this.DeviceConfigurationReader
                        .GetLatestDeviceConfigurationByDeviceAsync(organizationId, deviceId))
                    .ToEither($"DeviceId {deviceId} Not found"));

            return config;
        }

        /**
         * Use Async variants of Option and Either, which are just those same type classes wrapped in a `Task`.
         * Final `ToEither` converts the result back to an Either wrapped in a Task.
         */
        public Task<Either<string, DeviceConfiguration>> F2(string organizationId, string token, Guid deviceId) =>
            OptionalAsync(this.DeviceReader.GetDeviceAsync(organizationId, deviceId))
                .ToEither("Invalid Token")
                .Bind(d => OptionalAsync(this.GatewayReader.GetGatewayAsync(organizationId, d.GatewayId))
                    .ToEither("Invalid Token"))
                .Bind<Gateway>(g => g.ConfigurationToken != token ? g : "Invalid Token")
                .Bind(_ => OptionalAsync(this.DeviceConfigurationReader
                        .GetLatestDeviceConfigurationByDeviceAsync(organizationId, deviceId))
                    .ToEither($"DeviceId {deviceId} Not found"))
                .ToEither();

        /**
         * Remove redundant Option to Either conversion: Logically there is a bunch of upfront computation that
         * all has the same outcome, either a token is valid, or it isn't, so its represented by types here.
         */
        public Task<Either<string, DeviceConfiguration>> F3(string organizationId, string token, Guid deviceId) =>
            OptionalAsync(this.DeviceReader.GetDeviceAsync(organizationId, deviceId))
                .Bind(device => OptionalAsync(this.GatewayReader.GetGatewayAsync(organizationId, device.GatewayId)))
                .Filter(gateway => gateway.ConfigurationToken == token)
                .ToEither("Invalid Token")
                .Bind(_ => OptionalAsync(this.DeviceConfigurationReader
                        .GetLatestDeviceConfigurationByDeviceAsync(organizationId, deviceId))
                    .ToEither($"DeviceId {deviceId} Not found"))
                .ToEither();

        /**
         * Linq from statements are actually Binds, and select is a Map.
         * Note that linq doesn't have a way to express the change of Type from Option to Either.
         */
        public Task<Either<string, DeviceConfiguration>> F4(string organizationId, string token, Guid deviceId) =>
            (from _ in (
                    from device in OptionalAsync(this.DeviceReader.GetDeviceAsync(organizationId, deviceId))
                    from gateway in OptionalAsync(this.GatewayReader.GetGatewayAsync(organizationId, device.GatewayId))
                    where gateway.ConfigurationToken == token
                    select gateway).ToEither("Invalid Token")
                from deviceConfiguration in OptionalAsync(this.DeviceConfigurationReader
                        .GetLatestDeviceConfigurationByDeviceAsync(organizationId, deviceId))
                    .ToEither($"DeviceId {deviceId} Not found")
                select deviceConfiguration).ToEither();

        /**
         * Switching method signatures to return OptionAsync and EitherAsync cleans up the body.
         * Error left value is returned from one of the methods, and is mapped. It happens after token is validated
         * so its safe to display that to the user (see: enumeration attack).
         */
        public Task<Either<string, DeviceConfiguration>> F5(string organizationId, string token, Guid deviceId) =>
            (from _ in (
                    from device in this.DeviceReader.GetDevice(organizationId, deviceId)
                    from gateway in this.GatewayReader.GetGateway(organizationId, device.GatewayId)
                    where gateway.ConfigurationToken == token
                    select gateway).ToEither("Invalid Token")
                from deviceConfiguration in this.DeviceConfigurationReader
                    .GetLatestDeviceConfigurationByDevice(organizationId, deviceId)
                    .MapLeft(e => e.Message)
                select deviceConfiguration).ToEither();
    }
}
