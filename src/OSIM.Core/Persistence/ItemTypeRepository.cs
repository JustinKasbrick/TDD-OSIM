﻿using NHibernate;
using OSIM.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OSIM.Core.Persistence
{
	public interface IItemTypeRepository
	{
		int Save(ItemType itemType);

	}

	public class ItemTypeRepository : IItemTypeRepository
	{
		private readonly ISessionFactory _sessionFactory;

		public ItemTypeRepository(ISessionFactory sessionFactory)
		{
			_sessionFactory = sessionFactory;
		}

		public int Save(ItemType itemType)
		{
			int id;
			using (var session = _sessionFactory.OpenSession())
			{
				id = (int)session.Save(itemType);
				session.Flush();
			}
			return id;
		}
	}
}
