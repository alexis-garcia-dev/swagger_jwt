-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 15-07-2022 a las 19:06:49
-- Versión del servidor: 10.4.18-MariaDB
-- Versión de PHP: 7.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `jwt`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `bodega`
--

CREATE TABLE `bodega` (
  `BodegaId` int(11) NOT NULL,
  `Nombre` varchar(250) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `bodega`
--

INSERT INTO `bodega` (`BodegaId`, `Nombre`, `Estado`) VALUES
(1, 'BODEGA10', 1),
(2, 'BodegaDos', 1),
(3, 'bodega3', 1),
(4, 'store', 1),
(5, 'string', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `categoria`
--

CREATE TABLE `categoria` (
  `CategoriaId` int(11) NOT NULL,
  `Nombre` varchar(250) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `categoria`
--

INSERT INTO `categoria` (`CategoriaId`, `Nombre`, `Estado`) VALUES
(1, 'Medicia', 1),
(2, 'lacteos', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `entrada`
--

CREATE TABLE `entrada` (
  `EntradaId` int(11) NOT NULL,
  `ProductoId` int(11) NOT NULL,
  `BodegaId` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL,
  `FechaEntrada` date NOT NULL,
  `Cantidad` float NOT NULL,
  `EntradaTotal` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inventario`
--

CREATE TABLE `inventario` (
  `InventarioId` int(11) NOT NULL,
  `ProductoId` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL,
  `Cantidad` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `producto`
--

CREATE TABLE `producto` (
  `ProductoId` int(11) NOT NULL,
  `Nombre` varchar(250) NOT NULL,
  `CategoriaId` int(11) NOT NULL,
  `FechaCad` date NOT NULL,
  `FechaProd` date NOT NULL,
  `Precio` float NOT NULL,
  `Estado` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `producto`
--

INSERT INTO `producto` (`ProductoId`, `Nombre`, `CategoriaId`, `FechaCad`, `FechaProd`, `Precio`, `Estado`) VALUES
(1, 'Acetaminofen', 1, '2022-07-31', '2022-07-01', 23.1, 1),
(3, 'crema', 1, '2022-07-15', '2022-07-15', 120, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `roles`
--

CREATE TABLE `roles` (
  `RolesId` int(11) NOT NULL,
  `Nombre` varchar(250) NOT NULL,
  `Estado` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `roles`
--

INSERT INTO `roles` (`RolesId`, `Nombre`, `Estado`) VALUES
(8, 'USER', 0),
(9, 'ADMINISTRADOR', 1),
(10, 'string', 1),
(11, 'USER2', 1),
(12, 'SELLER', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `salida`
--

CREATE TABLE `salida` (
  `SalidaId` int(11) NOT NULL,
  `ProductoId` int(11) NOT NULL,
  `UsuarioId` int(11) NOT NULL,
  `FechaSalida` date NOT NULL,
  `Precio` float NOT NULL,
  `VentaTotal` float NOT NULL,
  `BodegaId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `UsuarioId` int(11) NOT NULL,
  `RolesId` int(11) NOT NULL,
  `Name` varchar(250) NOT NULL,
  `LastName` varchar(250) NOT NULL,
  `Email` varchar(250) NOT NULL,
  `Password` varchar(250) NOT NULL,
  `Address` varchar(250) NOT NULL,
  `UserName` varchar(250) NOT NULL,
  `Phone` varchar(250) NOT NULL,
  `Estado` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`UsuarioId`, `RolesId`, `Name`, `LastName`, `Email`, `Password`, `Address`, `UserName`, `Phone`, `Estado`) VALUES
(8, 10, 'pruvas', 'ves', 'alex@gmail.com', '$2a$11$Vuj64peoPOoFYq/fcN8Nr.LxtGycnQyj3m3gkmLF.9PuOgPeyFsEq', '87364', 'prueva', '7523537', 1),
(10, 10, 'alexis202', 'garcia', 'sv@gmail.com', '$2a$11$iXRYElXc.ZGVLwo4EPhUjuuZo6mHTZWcg5bACvcTO23na9MJiUjxW', 'sv sv', 'alexissv', '76374', 1);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `bodega`
--
ALTER TABLE `bodega`
  ADD PRIMARY KEY (`BodegaId`);

--
-- Indices de la tabla `categoria`
--
ALTER TABLE `categoria`
  ADD PRIMARY KEY (`CategoriaId`);

--
-- Indices de la tabla `entrada`
--
ALTER TABLE `entrada`
  ADD PRIMARY KEY (`EntradaId`),
  ADD KEY `BodegaId` (`BodegaId`),
  ADD KEY `UsuarioId` (`UsuarioId`),
  ADD KEY `ProductoId` (`ProductoId`);

--
-- Indices de la tabla `inventario`
--
ALTER TABLE `inventario`
  ADD PRIMARY KEY (`InventarioId`),
  ADD KEY `ProductoId` (`ProductoId`),
  ADD KEY `UsuarioId` (`UsuarioId`);

--
-- Indices de la tabla `producto`
--
ALTER TABLE `producto`
  ADD PRIMARY KEY (`ProductoId`),
  ADD KEY `CategoriaId` (`CategoriaId`);

--
-- Indices de la tabla `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`RolesId`);

--
-- Indices de la tabla `salida`
--
ALTER TABLE `salida`
  ADD PRIMARY KEY (`SalidaId`),
  ADD KEY `ProductoId` (`ProductoId`),
  ADD KEY `UsuarioId` (`UsuarioId`),
  ADD KEY `BodegaId` (`BodegaId`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`UsuarioId`),
  ADD KEY `RolesId` (`RolesId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `bodega`
--
ALTER TABLE `bodega`
  MODIFY `BodegaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `categoria`
--
ALTER TABLE `categoria`
  MODIFY `CategoriaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `entrada`
--
ALTER TABLE `entrada`
  MODIFY `EntradaId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `inventario`
--
ALTER TABLE `inventario`
  MODIFY `InventarioId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `producto`
--
ALTER TABLE `producto`
  MODIFY `ProductoId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `roles`
--
ALTER TABLE `roles`
  MODIFY `RolesId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `salida`
--
ALTER TABLE `salida`
  MODIFY `SalidaId` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `UsuarioId` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `entrada`
--
ALTER TABLE `entrada`
  ADD CONSTRAINT `entrada_ibfk_1` FOREIGN KEY (`UsuarioId`) REFERENCES `usuario` (`UsuarioId`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `entrada_ibfk_2` FOREIGN KEY (`ProductoId`) REFERENCES `producto` (`ProductoId`),
  ADD CONSTRAINT `entrada_ibfk_3` FOREIGN KEY (`BodegaId`) REFERENCES `bodega` (`BodegaId`);

--
-- Filtros para la tabla `inventario`
--
ALTER TABLE `inventario`
  ADD CONSTRAINT `inventario_ibfk_1` FOREIGN KEY (`ProductoId`) REFERENCES `producto` (`ProductoId`),
  ADD CONSTRAINT `inventario_ibfk_2` FOREIGN KEY (`UsuarioId`) REFERENCES `usuario` (`UsuarioId`);

--
-- Filtros para la tabla `producto`
--
ALTER TABLE `producto`
  ADD CONSTRAINT `producto_ibfk_1` FOREIGN KEY (`CategoriaId`) REFERENCES `categoria` (`CategoriaId`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Filtros para la tabla `salida`
--
ALTER TABLE `salida`
  ADD CONSTRAINT `salida_ibfk_1` FOREIGN KEY (`BodegaId`) REFERENCES `bodega` (`BodegaId`),
  ADD CONSTRAINT `salida_ibfk_2` FOREIGN KEY (`UsuarioId`) REFERENCES `usuario` (`UsuarioId`);

--
-- Filtros para la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`RolesId`) REFERENCES `roles` (`RolesId`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
