-- phpMyAdmin SQL Dump
-- version 4.7.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 10, 2019 at 03:21 PM
-- Server version: 10.1.28-MariaDB
-- PHP Version: 7.1.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `drink_database`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `load_data` (OUT `x` VARCHAR(200))  begin
select nama_produk into x from stok_barang where id = 16;
end$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `load_data_barang` (OUT `nama_produk` VARCHAR(255), OUT `merek_produk` VARCHAR(255))  begin
select nama_produk , merek_produk into nama_produk , merek_produk from stok_barang where id = 19;
end$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `load_data_from_barang` (OUT `x` VARCHAR(200), OUT `y` VARCHAR(200))  begin
select nama_produk , merek_produk into x , y from stok_barang where id = 16;
end$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `stok_barang`
--

CREATE TABLE `stok_barang` (
  `id` int(11) NOT NULL,
  `nama_produk` varchar(255) DEFAULT NULL,
  `jenis_produk` varchar(255) DEFAULT NULL,
  `merek_produk` varchar(100) DEFAULT NULL,
  `harga_beli` int(11) DEFAULT NULL,
  `harga_produk` int(11) NOT NULL,
  `jumlah_produk` int(11) DEFAULT NULL,
  `jumlah_beli` int(11) DEFAULT NULL,
  `total_penjualan` int(11) DEFAULT NULL,
  `tanggal_beli` varchar(255) DEFAULT NULL,
  `tanggal_kadaluarsa` varchar(255) DEFAULT NULL,
  `kode_supplier` char(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `stok_barang`
--

INSERT INTO `stok_barang` (`id`, `nama_produk`, `jenis_produk`, `merek_produk`, `harga_beli`, `harga_produk`, `jumlah_produk`, `jumlah_beli`, `total_penjualan`, `tanggal_beli`, `tanggal_kadaluarsa`, `kode_supplier`) VALUES
(31, 'Minuman White Coffe', 'Minuman', 'White Coffe', 2000, 5000, 60, 60, 54, '4/5/2019', '4/27/2019', NULL),
(35, 'Minute Maid Orange', 'Minuman', 'Minute Maid', 4000, 5000, 34, 50, 16, '5/1/2019', '5/31/2019', 'D03');

-- --------------------------------------------------------

--
-- Table structure for table `stok_penjualan`
--

CREATE TABLE `stok_penjualan` (
  `id` int(11) NOT NULL,
  `nama_pembeli` varchar(255) DEFAULT NULL,
  `total_harga` int(11) DEFAULT NULL,
  `nama_produk` varchar(255) DEFAULT NULL,
  `jenis_produk` varchar(255) DEFAULT NULL,
  `merek_produk` varchar(100) DEFAULT NULL,
  `jumlah_penjualan` int(11) DEFAULT NULL,
  `harga_produk` int(11) DEFAULT NULL,
  `penjualan` varchar(100) DEFAULT NULL,
  `total_untung` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `stok_penjualan`
--

INSERT INTO `stok_penjualan` (`id`, `nama_pembeli`, `total_harga`, `nama_produk`, `jenis_produk`, `merek_produk`, `jumlah_penjualan`, `harga_produk`, `penjualan`, `total_untung`) VALUES
(40, 'Daffa', 80000, 'Minuman White Coffe', 'Minuman', 'White Coffe', 20, 4000, '04/05/2019', 40000),
(41, 'Daffa Naufal Rachmat', 50000, 'Minuman White Coffe', 'Minuman', 'White Coffe', 10, 5000, '04/05/2019', 30000),
(43, 'Rahmat', 80000, 'Minuman White Coffe', 'Minuman', 'White Coffe', 16, 5000, '04/06/2019', 48000),
(46, 'Daffa', 108000, 'Minuman Jahe Mas Sepur', 'Minuman', 'Mas Sepur', 18, 6000, '04/30/2019', 54000),
(47, 'Daffa', 60000, 'White Coffe Luwak', 'Minuman', 'Luwak', 20, 3000, '05/01/2019', 40000),
(48, 'Daffa', 80000, 'Minute Maid Orange', 'Minuman', 'Minute Maid', 16, 5000, '05/01/2019', 16000);

-- --------------------------------------------------------

--
-- Table structure for table `supplier`
--

CREATE TABLE `supplier` (
  `kode_supplier` char(10) NOT NULL,
  `nama_supplier` varchar(200) DEFAULT NULL,
  `alamat_supplier` varchar(200) DEFAULT NULL,
  `telepon_supplier` varchar(20) DEFAULT NULL,
  `email_supplier` varchar(255) DEFAULT NULL,
  `tanggal_daftar` varchar(200) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `supplier`
--

INSERT INTO `supplier` (`kode_supplier`, `nama_supplier`, `alamat_supplier`, `telepon_supplier`, `email_supplier`, `tanggal_daftar`) VALUES
('D03', 'A-Team Distributor', 'Jl. bla bla bla', '08xxxxx', 'naufalrachmat@gmail.com', '5/1/2019');

-- --------------------------------------------------------

--
-- Table structure for table `untung_rugi_bulanan`
--

CREATE TABLE `untung_rugi_bulanan` (
  `id` int(11) NOT NULL,
  `bulan` varchar(5) DEFAULT NULL,
  `total_semua` int(11) DEFAULT NULL,
  `status` enum('untung','tidak untung dan rugi','rugi') DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(255) DEFAULT NULL,
  `password` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `username`, `password`) VALUES
(11, 'DAFFA', 'wgFjZAnXDfk6NyrYZ+zJHQ=='),
(12, 'DARRA', 'wgFjZAnXDfk6NyrYZ+zJHQ==');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `stok_barang`
--
ALTER TABLE `stok_barang`
  ADD PRIMARY KEY (`id`),
  ADD KEY `kode_supplier` (`kode_supplier`);

--
-- Indexes for table `stok_penjualan`
--
ALTER TABLE `stok_penjualan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `supplier`
--
ALTER TABLE `supplier`
  ADD PRIMARY KEY (`kode_supplier`);

--
-- Indexes for table `untung_rugi_bulanan`
--
ALTER TABLE `untung_rugi_bulanan`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `stok_barang`
--
ALTER TABLE `stok_barang`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT for table `stok_penjualan`
--
ALTER TABLE `stok_penjualan`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=49;

--
-- AUTO_INCREMENT for table `untung_rugi_bulanan`
--
ALTER TABLE `untung_rugi_bulanan`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `stok_barang`
--
ALTER TABLE `stok_barang`
  ADD CONSTRAINT `stok_barang_ibfk_1` FOREIGN KEY (`kode_supplier`) REFERENCES `supplier` (`kode_supplier`) ON DELETE SET NULL ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
