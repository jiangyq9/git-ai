//! Notes backend module.
//!
//! `notes::db` provides the dedicated `~/.git-ai/internal/notes-db` SQLite store
//! used by the HTTP notes backend as both a write queue and a local read cache.

pub mod db;
