/*
 * base.h
 *
 *  Created on: 31 mar 2021
 *
 */
#ifndef _BASE_H
#define _BASE_H

// add headers that you want to pre-compile here

#if defined(_WIN32) || defined (_WIN64)
typedef unsigned __int64 id64_t;
#else
typedef unsigned long long int id64_t;
#endif

typedef unsigned int id_t;
typedef unsigned int word_t;
typedef unsigned short short_t;

#ifndef BM_DEFAULT_POOL_SIZE
# define BM_DEFAULT_POOL_SIZE 4096
#endif

typedef unsigned short gap_word_t;

/**
 DeBruijn majic table
 @internal
 */
template<bool T> struct DeBruijn_bit_position
{
	static const unsigned _multiply[32];
};

template<bool T>
const unsigned DeBruijn_bit_position<T>::_multiply[32] =
{ 0, 1, 28, 2, 29, 14, 24, 3, 30, 22, 20, 15, 25, 17, 4, 8, 31, 27, 13, 23, 21,
		19, 16, 7, 26, 12, 18, 6, 11, 5, 10, 9 };

/**
 Structure keeps index of first right 1 bit for every byte.
 @ingroup bitfunc
 */
template<bool T> struct first_bit_table
{
	static const signed char _idx[256];
};

template<bool T>
const signed char first_bit_table<T>::_idx[256] =
{ -1, 0, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4,
		4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5,
		5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
		6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
		6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6,
		6, 6, 6, 6, 6, 6, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
		7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
		7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
		7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
		7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7,
		7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, };

//---------------------------------------------------------------------

/** Structure to aid in counting bits
 table contains count of bits in 0-255 diapason of numbers

 @ingroup bitfunc
 */
template<bool T> struct bit_count_table
{
	static const unsigned char _count[256];
};

template<bool T>
const unsigned char bit_count_table<T>::_count[256] =
{ 0, 1, 1, 2, 1, 2, 2, 3, 1, 2, 2, 3, 2, 3, 3, 4, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3,
		3, 4, 3, 4, 4, 5, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3,
		3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3,
		3, 4, 3, 4, 4, 5, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3,
		3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5,
		5, 6, 5, 6, 6, 7, 1, 2, 2, 3, 2, 3, 3, 4, 2, 3, 3, 4, 3, 4, 4, 5, 2, 3,
		3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 2, 3, 3, 4, 3, 4, 4, 5, 3, 4,
		4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 2, 3,
		3, 4, 3, 4, 4, 5, 3, 4, 4, 5, 4, 5, 5, 6, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5,
		5, 6, 5, 6, 6, 7, 3, 4, 4, 5, 4, 5, 5, 6, 4, 5, 5, 6, 5, 6, 6, 7, 4, 5,
		5, 6, 5, 6, 6, 7, 5, 6, 6, 7, 6, 7, 7, 8 };

//---------------------------------------------------------------------

/** Structure for LZCNT constants (4-bit)
 @ingroup bitfunc
 */
template<bool T> struct lzcnt_table
{
	static unsigned char const _lut[16];
};

template<bool T>
const unsigned char lzcnt_table<T>::_lut[16] =
{ 32U, 31U, 30U, 30U, 29U, 29U, 29U, 29U, 28U, 28U, 28U, 28U, 28U, 28U,
		28U, 28U };

/** Structure for TZCNT constants
 @ingroup bitfunc
 */
template<bool T> struct tzcnt_table
{
	static unsigned char const _lut[37];
};

template<bool T>
const unsigned char tzcnt_table<T>::_lut[37] =
{ 32, 0, 1, 26, 2, 23, 27, 0, 3, 16, 24, 30, 28, 11, 0, 13, 4, 7, 17, 0, 25, 22,
		31, 15, 29, 10, 12, 6, 0, 21, 14, 9, 5, 20, 8, 19, 18 };

#endif //_BASE_H
