using OSIM.Core.Entities;
using OSIM.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using AutoFixture;
using AutoFixture.Xunit2;
using FakeItEasy;
using NHibernate;

namespace OSIM.UnitTests.OSIM.Core
{
	public class WhenWorkingWithTheItemTypeRepository
	{
		public class AndSavingAValidItemType
		{
			[Theory]
			[ItemTypeRepositoryArrangement]
			public void ThenAValidItemTypeIdShouldBeReturned(IItemTypeRepository sut, ItemType itemType, int itemTypeId)
			{
				// arrange

				// act 
				var result = sut.Save(itemType);

				// assert
				result.Should().Be(itemTypeId);
			}
		}

		private class ItemTypeRepositoryArrangement : AutoDataAttribute
		{	
			public ItemTypeRepositoryArrangement()
				: base(() => new Fixture().Customize(new ItemTypeRepositoryCustomization()))
			{ }
		}

		private class ItemTypeRepositoryCustomization : ICustomization
		{
			public void Customize(IFixture fixture)
			{
				var itemType = fixture.Freeze<ItemType>();
				var itemTypeId = fixture.Freeze<int>();

				var sessionFactory = A.Fake<ISessionFactory>();
				var session = A.Fake<ISession>();

				A.CallTo(() => sessionFactory.OpenSession()).Returns(session);
				A.CallTo(() => session.Save(itemType)).Returns(itemTypeId);

				fixture.Register<IItemTypeRepository>(() =>
				{
					return new ItemTypeRepository(sessionFactory);
				});
			}
		}
	}
}
