<h1 align="center">MapleCosmicDashboard</h1>

> This is a fun, small dashboard for MapleStory Server. It's built on C# Fullstack Blazor, which is a framework I love.
> I want people to know more about Cosmic and Blazor.
> Since this is a hobby project, don't expect much.

## 🚀 Usage
Run this SQL for the dashboard to work.
```sh
CREATE TABLE IF NOT EXISTS `coupons` (
`id` int(11) NOT NULL AUTO_INCREMENT,
`accountId` int(11) NOT NULL,
`code` varchar(12) NOT NULL,
`amount` int(4) NOT NULL,
`used` tinyint(1) NOT NULL DEFAULT '0',
`createDate` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
`redeemDate` timestamp NULL,
PRIMARY KEY (`id`),
CONSTRAINT `fk_coupons_account` FOREIGN KEY (`accountId`) REFERENCES `accounts`(`id`) ON UPDATE CASCADE ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

ALTER TABLE `accounts` 
ADD COLUMN `lastRedeem` TIMESTAMP NULL AFTER lastlogin;

ALTER TABLE `accounts` 
ADD COLUMN `streak` int(3) NOT NULL DEFAULT '0' AFTER lastRedeem;
```
The password is BCrypt by default, so make sure your server settings are the same.


## 🤝 Contributing

Contributions, issues and feature requests are welcome.<br />
Feel free to check [issues page](https://github.com/truongdatnhan/MapleCosmicDashboard/issues) and make pull request if you want to contribute.<br />

## Author

👤 **TRUONG DAT NHAN (truongdatnhan)**

- Github: [@truongdatnhan](https://github.com/truongdatnhan)
- LinkedIn: [Nhan Truong Dat](https://www.linkedin.com/in/nhantruongdat/)

## 📝 License

Copyright © 2024 [Truong Dat Nhan](https://github.com/truongdatnhan).<br />
This project is [MIT](https://github.com/truongdatnhan/MapleCosmicDashboard/blob/master/LICENSE) licensed.

---

_This README was generated with ❤️ by [readme-md-generator](https://github.com/kefranabg/readme-md-generator)_
